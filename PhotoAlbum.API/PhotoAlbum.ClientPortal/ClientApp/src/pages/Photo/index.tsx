import React, { useState, useEffect } from 'react';
import { PageContainer } from '@ant-design/pro-layout';
import { Button, Card, Form, message, Image, Popconfirm } from 'antd';
import { useHistory, useIntl, useModel } from 'umi';
import styles from './index.less';
import { addPhoto, deletePhoto, getPhoto, updatePhoto, upsertMetadata } from '@/services/api';
import ProForm, { ProFormSelect, ProFormText } from '@ant-design/pro-form';
import { ArrowLeftOutlined, CloseOutlined, DeleteOutlined, InboxOutlined, ThunderboltOutlined } from '@ant-design/icons';
import Dragger from 'antd/lib/upload/Dragger';
import { RcFile } from 'antd/lib/upload';
import { convertBlobToUrl, determineMimeType } from '@/utils/utils';

export default (props: any): React.ReactNode => {
    const { initialState, setInitialState } = useModel('@@initialState');

    const [fileList, setFileList] = useState<any[]>([]);

    const [form] = Form.useForm();
    const history = useHistory();

    const [loading, setLoading] = useState<boolean>(true);
    const [photo, setPhoto] = useState<any>(undefined);
    const [mode, setMode] = useState<any>('add');

    const [fileUrl, setFileUrl] = useState<any>(undefined);
    const [fileData, setFileData] = useState<any>({});

    useEffect(() => {
        const params = new URLSearchParams(props.location.search);
        const id = params.get('id');

        if (id) {
            setMode('edit');
            loadAlbum(id);
        }
        else {
            setLoading(false);
        }
    }, []);

    useEffect(() => {
        console.log(fileList)
    }, [fileList]);

    useEffect(() => {
        if (photo?.filename && photo?.data) {
            let url = convertBlobToUrl(photo.data, photo.filename)
            console.log(url)
            setFileUrl(url);
        }

        form.resetFields();
    }, [photo]);

    const loadAlbum = async (id: string) => {
        try {
            const response = await getPhoto(id);
            if (response) setPhoto(response);
        } catch (error) {
            message.error("Unable to load the photo!");
        }
        setLoading(false);
    }

    const handleSubmit = async (values: any) => {
        setLoading(true);
        try {
            let photoId = photo?.id;

            if (mode === 'add') {
                const response: any = await addPhoto(fileData);
                photoId = response.id;

                await upsertMetadata({ photoId, ...values });

                message.success("Successfully added the photo!");
            } else {
                if (fileData?.filename)
                    await updatePhoto(photo.id, { ...fileData });

                await upsertMetadata({ photoId, ...values });

                message.success("Successfully updated the photo!");
            }
            history.push(`/photo/view?id=${photoId}`)
        } catch (error: any) {
            message.error('Failed to add the photo.');
        }
        setLoading(false);
    };

    const handleDeleteClicked = async () => {
        setLoading(true);
        try {
            await deletePhoto(photo.id);
            message.success("Successfully deleted the photo!");
            history.push(`/photos`)
        } catch (error: any) {
            message.error("Failed to delete the photo!");
        }
        setLoading(false);
    }

    const handleCancelClicked = () => {
        if (photo?.id) history.push(`/photo/view?id=${photo.id}`)
        else history.push(`/photos`);
    }

    const renderActions = (): JSX.Element => <>
        {mode === 'edit' && initialState?.currentUser?.id && photo?.createdByUserId === initialState?.currentUser?.id && <Popconfirm
            title="Are you sure you want to delete this photo?"
            onConfirm={handleDeleteClicked}
            okText="Yes"
            cancelText="Cancel"
        >
            <Button type='default' icon={<DeleteOutlined />} >Delete Photo</Button></Popconfirm>}
        <Button type='default' icon={<ArrowLeftOutlined />} onClick={handleCancelClicked}>Back</Button>
    </>

    const onChange = ({ fileList: newFileList }: any) => {
        setFileList(newFileList);
    };

    const handleAdditionalDocumentUpload = (docData: RcFile) => {
        if (docData.size > 10485760) {
            return 'error';
        }

        const reader = new FileReader();
        const fileByteArray: any[] = [];

        reader.readAsArrayBuffer(docData);
        reader.onloadend = (evt: any) => {
            if (evt.target.readyState === FileReader.DONE) {
                const arrayBuffer = evt.target.result;
                const array = new Uint8Array(arrayBuffer as ArrayBuffer);
                for (let i = 0; i < array.length; i++) {
                    fileByteArray.push(array[i]);
                }

                let url = URL.createObjectURL(docData);
                setFileUrl(url);

                setFileData({
                    filename: docData && docData.name,
                    data: fileByteArray
                });
            }
        };

        return 'uploading';
    }

    const uploadProps = {
        name: 'file',
        action: handleAdditionalDocumentUpload,
        accept: ".bmp, .ico, .jpg, .jpeg, .tif, .gif, .png",
        onChange(info: any) {
            const { status } = info.file;
            if (status !== 'uploading') {
                console.log(info.file, info.fileList);
            }
            if (status === 'done') {
                message.success(`${info.file.name} file uploaded successfully.`);
            } else if (status === 'error') {
                message.error(`${info.file.name} file upload failed.`);
            }
        },
        locale: {
            previewFile: 'Preview',
            removeFile: 'Remove'
        },
        onDrop(e: any) {
            console.log('Dropped files', e.dataTransfer.files);
        },
    };

    return (
        <PageContainer
            extra={renderActions()}
        >
            <Card loading={loading}>
                <div style={{ textAlign: 'center' }}>
                    <Image
                        style={{ width: 400 }}
                        src={fileUrl || "error"}
                        fallback="data:image/png;base64,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"
                    />
                </div>
                <br />
                <Dragger {...uploadProps} showUploadList={false}
                    onChange={onChange}>
                    <p className="ant-upload-drag-icon">
                        <InboxOutlined />
                    </p>
                    <p className="ant-upload-text">Click or drag a file to this area to upload</p>
                    <p className="ant-upload-hint">
                        Supported file types are {uploadProps.accept}.
                    </p>
                </Dragger>
                <br />
                <ProForm
                    form={form}
                    initialValues={photo?.metadata}
                    submitter={{
                        render: (props, dom) => {
                            return <>
                                <Button loading={loading} disabled={loading} type='primary' icon={<ThunderboltOutlined />}
                                    style={{ width: '100%', marginTop: 10 }}
                                    onClick={() => { form.submit() }}>{mode === 'edit' ? 'Update' : 'Add'}</Button>
                                <Button type='default' icon={<CloseOutlined />} style={{ width: '100%', marginTop: 10 }}
                                    onClick={handleCancelClicked}>Cancel</Button>
                            </>
                        }
                    }}
                    onFinish={async (values) => {
                        await handleSubmit(values as API.LoginParams);
                    }}
                >
                    <ProFormText
                        label='Geolocation'
                        disabled={loading}
                        name="geolocation"
                        fieldProps={{
                            size: 'large',
                        }}
                        placeholder='Geolocation'
                    />
                    <ProFormSelect
                        label='Tags'
                        mode={'tags'}
                        disabled={loading}
                        name="tags"
                        fieldProps={{
                            size: 'large',
                        }}
                        placeholder='Tags'
                    />
                </ProForm>
            </Card>
        </PageContainer>
    );
};
