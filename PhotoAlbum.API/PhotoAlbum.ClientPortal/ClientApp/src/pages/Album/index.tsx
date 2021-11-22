import React, { useState, useEffect } from 'react';
import { PageContainer } from '@ant-design/pro-layout';
import { Button, Card, Form, message, Image, Popconfirm } from 'antd';
import { useHistory, useIntl, useModel } from 'umi';
import styles from './index.less';
import { addAlbum, deleteAlbum, getAlbum, getAvailablePhotos, getPhotosForAlbum, relinkPhotos, updateAlbum } from '@/services/api';
import ProForm, { ProFormText } from '@ant-design/pro-form';
import { ArrowLeftOutlined, CloseOutlined, DeleteOutlined, ThunderboltOutlined } from '@ant-design/icons';
import { convertBlobToUrl } from '@/utils/utils';

export default (props: any): React.ReactNode => {
    const { initialState, setInitialState } = useModel('@@initialState');

    const intl = useIntl();
    const [form] = Form.useForm();
    const history = useHistory();

    const [loading, setLoading] = useState<boolean>(true);
    const [loadingLinked, setLoadingLinked] = useState<boolean>(true);
    const [loadingAvailable, setLoadingAvailable] = useState<boolean>(true);
    const [album, setAlbum] = useState<any>(undefined);
    const [mode, setMode] = useState<any>('add');

    const [linkedPhotos, setLinkedPhotos] = useState<any[]>([]);
    const [availablePhotos, setAvailablePhotos] = useState<any[]>([]);

    useEffect(() => {
        const params = new URLSearchParams(props.location.search);
        const id = params.get('id');

        loadAvailablePhotos();

        if (id) {
            setMode('edit');
            loadAlbum(id);
            loadPhotos(id);
        }
        else {
            setLoading(false);
            setLoadingLinked(false);
        }
    }, []);

    useEffect(() => {
        form.resetFields();
    }, [album]);

    const loadAlbum = async (id: string) => {
        try {
            const response = await getAlbum(id);
            if (response) setAlbum(response);
        } catch (error) {
            message.error("Unable to load the album!");
        }
        setLoading(false);
    }

    const loadPhotos = async (id: string) => {
        try {
            const response = await getPhotosForAlbum(id);
            if (response) setLinkedPhotos(response);
        } catch (error) {
            message.error("Unable to load the album photos!");
        }
        setLoadingLinked(false);
    }

    const loadAvailablePhotos = async () => {
        try {
            const response = await getAvailablePhotos();
            if (response) setAvailablePhotos(response);
        } catch (error) {
            message.error("Unable to load the available photos!");
        }
        setLoadingAvailable(false);
    }

    const handleSubmit = async (values: any) => {
        setLoading(true);
        try {
            let albumId = album?.id;

            if (mode === 'add') {
                const response: any = await addAlbum({ ...values });
                message.success("Successfully added the album!");
                albumId = response?.id;
            } else {
                await updateAlbum(album.id, { ...values });
                message.success("Successfully updated the album!");
            }

            let photoIds = linkedPhotos.map(x => x.id);
            await relinkPhotos({ albumId, photoIds });

            history.push(`/album/view?id=${albumId}`)
        } catch (error: any) {
            const defaultLoginFailureMessage = error?.message || intl.formatMessage({
                id: 'pages.login.failure',
            });
            message.error(defaultLoginFailureMessage);
        }
        setLoading(false);
    };

    const handleDeleteClicked = async () => {
        setLoading(true);
        try {
            await deleteAlbum(album.id);
            message.success("Successfully deleted the album!");
            history.push(`/albums`)
        } catch (error: any) {
            message.error("Failed to delete the album!");
        }
        setLoading(false);
    }

    const handleCancelClicked = () => {
        if (album?.id) history.push(`/album/view?id=${album.id}`)
        else history.push(`/albums`);
    }

    const renderActions = (): JSX.Element => <>
        {mode === 'edit' && initialState?.currentUser?.id && album?.createdByUserId === initialState?.currentUser?.id && <Popconfirm
            title="Are you sure you want to delete this album?"
            onConfirm={handleDeleteClicked}
            okText="Yes"
            cancelText="Cancel"
        >
            <Button type='default' icon={<DeleteOutlined />} >Delete Album</Button></Popconfirm>}
        <Button type='default' icon={<ArrowLeftOutlined />} onClick={handleCancelClicked}>Back</Button>
    </>

    const handleRemoveClicked = (photo: any) => {
        let newAvailableList = [...availablePhotos];
        newAvailableList.push(photo)

        let newLinkedList = [...linkedPhotos];
        let index = newLinkedList.findIndex(x => x.id === photo.id);
        if (index > -1)
            newLinkedList.splice(index, 1)

        setLinkedPhotos(newLinkedList);
        setAvailablePhotos(newAvailableList);
    }

    const handleAddClicked = (photo: any) => {
        let newLinkedList = [...linkedPhotos];
        newLinkedList.push(photo)


        let newAvailableList = [...availablePhotos];
        let index = newAvailableList.findIndex(x => x.id === photo.id);
        if (index > -1)
            newAvailableList.splice(index, 1)

        setLinkedPhotos(newLinkedList);
        setAvailablePhotos(newAvailableList);
    }

    const renderAvailablePhoto = (photo: any): JSX.Element => {
        let fileUrl = 'error';
        if (photo?.filename && photo?.data) {
            fileUrl = convertBlobToUrl(photo.data, photo.filename)
        }
        return <Card.Grid
            key={photo.id}
            hoverable
            style={{ width: 230, padding: 15 }}
        >
            <div onClick={() => handleAddClicked(photo)} style={{ textAlign: 'center', padding: 0 }}>
                <Image
                    height={200}
                    src={fileUrl}
                    preview={false}
                    fallback="data:image/png;base64,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"
                />
                <div style={{ fontSize: '0.8em', textAlign: 'left' }}>
                    <span style={{ fontWeight: 'bold' }}>File:</span> {photo?.filename || 'N/A'}
                    <br />
                    <span style={{ fontWeight: 'bold' }}>Geolocation:</span> {photo?.metadata?.geolocation || 'N/A'}
                    <br />
                    <span style={{ fontWeight: 'bold' }}>Tags: </span> {photo?.metadata?.tags?.join(", ") || 'N/A'}
                </div>
            </div>
        </Card.Grid>
    }

    const renderSelectedPhoto = (photo: any): JSX.Element => {
        let fileUrl = 'error';
        if (photo?.filename && photo?.data) {
            fileUrl = convertBlobToUrl(photo.data, photo.filename)
        }
        return <Card.Grid
            key={photo.id}
            hoverable
            style={{ width: 230, padding: 15 }}
        >
            <div onClick={() => handleRemoveClicked(photo)} style={{ textAlign: 'center', padding: 0 }}>
                <Image
                    height={200}
                    src={fileUrl}
                    preview={false}
                    fallback="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAMIAAADDCAYAAADQvc6UAAABRWlDQ1BJQ0MgUHJvZmlsZQAAKJFjYGASSSwoyGFhYGDIzSspCnJ3UoiIjFJgf8LAwSDCIMogwMCcmFxc4BgQ4ANUwgCjUcG3awyMIPqyLsis7PPOq3QdDFcvjV3jOD1boQVTPQrgSkktTgbSf4A4LbmgqISBgTEFyFYuLykAsTuAbJEioKOA7DkgdjqEvQHEToKwj4DVhAQ5A9k3gGyB5IxEoBmML4BsnSQk8XQkNtReEOBxcfXxUQg1Mjc0dyHgXNJBSWpFCYh2zi+oLMpMzyhRcASGUqqCZ16yno6CkYGRAQMDKMwhqj/fAIcloxgHQqxAjIHBEugw5sUIsSQpBobtQPdLciLEVJYzMPBHMDBsayhILEqEO4DxG0txmrERhM29nYGBddr//5/DGRjYNRkY/l7////39v///y4Dmn+LgeHANwDrkl1AuO+pmgAAADhlWElmTU0AKgAAAAgAAYdpAAQAAAABAAAAGgAAAAAAAqACAAQAAAABAAAAwqADAAQAAAABAAAAwwAAAAD9b/HnAAAHlklEQVR4Ae3dP3PTWBSGcbGzM6GCKqlIBRV0dHRJFarQ0eUT8LH4BnRU0NHR0UEFVdIlFRV7TzRksomPY8uykTk/zewQfKw/9znv4yvJynLv4uLiV2dBoDiBf4qP3/ARuCRABEFAoBEgghggQAQZQKAnYEaQBAQaASKIAQJEkAEEegJmBElAoBEgghggQAQZQKAnYEaQBAQaASKIAQJEkAEEegJmBElAoBEgghggQAQZQKAnYEaQBAQaASKIAQJEkAEEegJmBElAoBEgghggQAQZQKAnYEaQBAQaASKIAQJEkAEEegJmBElAoBEgghggQAQZQKAnYEaQBAQaASKIAQJEkAEEegJmBElAoBEgghggQAQZQKAnYEaQBAQaASKIAQJEkAEEegJmBElAoBEgghggQAQZQKAnYEaQBAQaASKIAQJEkAEEegJmBElAoBEgghggQAQZQKAnYEaQBAQaASKIAQJEkAEEegJmBElAoBEgghggQAQZQKAnYEaQBAQaASKIAQJEkAEEegJmBElAoBEgghggQAQZQKAnYEaQBAQaASKIAQJEkAEEegJmBElAoBEgghggQAQZQKAnYEaQBAQaASKIAQJEkAEEegJmBElAoBEgghggQAQZQKAnYEaQBAQaASKIAQJEkAEEegJmBElAoBEgghgg0Aj8i0JO4OzsrPv69Wv+hi2qPHr0qNvf39+iI97soRIh4f3z58/u7du3SXX7Xt7Z2enevHmzfQe+oSN2apSAPj09TSrb+XKI/f379+08+A0cNRE2ANkupk+ACNPvkSPcAAEibACyXUyfABGm3yNHuAECRNgAZLuYPgEirKlHu7u7XdyytGwHAd8jjNyng4OD7vnz51dbPT8/7z58+NB9+/bt6jU/TI+AGWHEnrx48eJ/EsSmHzx40L18+fLyzxF3ZVMjEyDCiEDjMYZZS5wiPXnyZFbJaxMhQIQRGzHvWR7XCyOCXsOmiDAi1HmPMMQjDpbpEiDCiL358eNHurW/5SnWdIBbXiDCiA38/Pnzrce2YyZ4//59F3ePLNMl4PbpiL2J0L979+7yDtHDhw8vtzzvdGnEXdvUigSIsCLAWavHp/+qM0BcXMd/q25n1vF57TYBp0a3mUzilePj4+7k5KSLb6gt6ydAhPUzXnoPR0dHl79WGTNCfBnn1uvSCJdegQhLI1vvCk+fPu2ePXt2tZOYEV6/fn31dz+shwAR1sP1cqvLntbEN9MxA9xcYjsxS1jWR4AIa2Ibzx0tc44fYX/16lV6NDFLXH+YL32jwiACRBiEbf5KcXoTIsQSpzXx4N28Ja4BQoK7rgXiydbHjx/P25TaQAJEGAguWy0+2Q8PD6/Ki4R8EVl+bzBOnZY95fq9rj9zAkTI2SxdidBHqG9+skdw43borCXO/ZcJdraPWdv22uIEiLA4q7nvvCug8WTqzQveOH26fodo7g6uFe/a17W3+nFBAkRYENRdb1vkkz1CH9cPsVy/jrhr27PqMYvENYNlHAIesRiBYwRy0V+8iXP8+/fvX11Mr7L7ECueb/r48eMqm7FuI2BGWDEG8cm+7G3NEOfmdcTQw4h9/55lhm7DekRYKQPZF2ArbXTAyu4kDYB2YxUzwg0gi/41ztHnfQG26HbGel/crVrm7tNY+/1btkOEAZ2M05r4FB7r9GbAIdxaZYrHdOsgJ/wCEQY0J74TmOKnbxxT9n3FgGGWWsVdowHtjt9Nnvf7yQM2aZU/TIAIAxrw6dOnAWtZZcoEnBpNuTuObWMEiLAx1HY0ZQJEmHJ3HNvGCBBhY6jtaMoEiJB0Z29vL6ls58vxPcO8/zfrdo5qvKO+d3Fx8Wu8zf1dW4p/cPzLly/dtv9Ts/EbcvGAHhHyfBIhZ6NSiIBTo0LNNtScABFyNiqFCBChULMNNSdAhJyNSiECRCjUbEPNCRAhZ6NSiAARCjXbUHMCRMjZqBQiQIRCzTbUnAARcjYqhQgQoVCzDTUnQIScjUohAkQo1GxDzQkQIWejUogAEQo121BzAkTI2agUIkCEQs021JwAEXI2KoUIEKFQsw01J0CEnI1KIQJEKNRsQ80JECFno1KIABEKNdtQcwJEyNmoFCJAhELNNtScABFyNiqFCBChULMNNSdAhJyNSiECRCjUbEPNCRAhZ6NSiAARCjXbUHMCRMjZqBQiQIRCzTbUnAARcjYqhQgQoVCzDTUnQIScjUohAkQo1GxDzQkQIWejUogAEQo121BzAkTI2agUIkCEQs021JwAEXI2KoUIEKFQsw01J0CEnI1KIQJEKNRsQ80JECFno1KIABEKNdtQcwJEyNmoFCJAhELNNtScABFyNiqFCBChULMNNSdAhJyNSiECRCjUbEPNCRAhZ6NSiAARCjXbUHMCRMjZqBQiQIRCzTbUnAARcjYqhQgQoVCzDTUnQIScjUohAkQo1GxDzQkQIWejUogAEQo121BzAkTI2agUIkCEQs021JwAEXI2KoUIEKFQsw01J0CEnI1KIQJEKNRsQ80JECFno1KIABEKNdtQcwJEyNmoFCJAhELNNtScABFyNiqFCBChULMNNSdAhJyNSiEC/wGgKKC4YMA4TAAAAABJRU5ErkJggg=="
                />
                <div style={{ fontSize: '0.8em', textAlign: 'left' }}>
                    <span style={{ fontWeight: 'bold' }}>File:</span> {photo?.filename || 'N/A'}
                    <br />
                    <span style={{ fontWeight: 'bold' }}>Geolocation:</span> {photo?.metadata?.geolocation || 'N/A'}
                    <br />
                    <span style={{ fontWeight: 'bold' }}>Tags: </span> {photo?.metadata?.tags?.join(", ") || 'N/A'}
                </div>
            </div>
        </Card.Grid>
    }

    const renderNoPhotos = (): JSX.Element => <>NO PHOTOS</>

    return (
        <PageContainer
            extra={renderActions()}
        >
            <Card loading={loading}>
                <ProForm
                    form={form}
                    initialValues={album}
                    submitter={{
                        render: (props, dom) => {
                            return <>
                                <Button loading={loading} disabled={loading} type='primary' icon={<ThunderboltOutlined />}
                                    style={{ width: '100%', marginTop: 10 }}
                                    onClick={() => { form.submit() }}>{mode === 'edit' ? 'Update' : 'Create'}</Button>
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
                        label='Name'
                        disabled={loading}
                        name="name"
                        fieldProps={{
                            size: 'large',
                        }}
                        placeholder='Name'
                        rules={[
                            {
                                required: true,
                                message: "Please enter a name for album!",
                            },
                        ]}
                    />
                    <ProFormText
                        label='Description'
                        name="description"
                        disabled={loading}
                        fieldProps={{
                            size: 'large',
                        }}
                        placeholder='Description'
                        rules={[
                            {
                                required: true,
                                message: "Please enter a description for album!",
                            },
                        ]}
                    />
                    <Card title="Photos in Album" style={{ maxHeight: 600, overflowY: 'auto' }} loading={loadingLinked}>
                        {linkedPhotos.map(photo => renderSelectedPhoto(photo))}
                        {linkedPhotos.length === 0 && renderNoPhotos()}
                    </Card>
                    <br />
                    <Card title="Available Photos" style={{ maxHeight: 600, overflowY: 'auto' }} loading={loadingAvailable}>
                        {availablePhotos.map(photo => renderAvailablePhoto(photo))}
                        {availablePhotos.length === 0 && renderNoPhotos()}
                    </Card>
                    <br />
                </ProForm>
            </Card>
        </PageContainer>
    );
};
