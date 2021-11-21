import React, { useState, useEffect } from 'react';
import { PageContainer } from '@ant-design/pro-layout';
import { Button, Card, Form, message } from 'antd';
import { useHistory, useIntl, useModel } from 'umi';
import styles from './index.less';
import { addAlbum, deleteAlbum, getAlbum, updateAlbum } from '@/services/api';
import ProForm, { ProFormText } from '@ant-design/pro-form';
import { ArrowLeftOutlined, CloseOutlined, DeleteOutlined, ThunderboltOutlined } from '@ant-design/icons';

export default (props: any): React.ReactNode => {
    const { initialState, setInitialState } = useModel('@@initialState');

    console.log("Album Edit=> ", initialState?.currentUser?.id);

    const intl = useIntl();
    const [form] = Form.useForm();
    const history = useHistory();

    const [loading, setLoading] = useState<boolean>(true);
    const [album, setAlbum] = useState<any>(undefined);
    const [mode, setMode] = useState<any>('add');

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
        console.log("ALBUM CHANGED!", album)
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

    const handleSubmit = async (values: any) => {
        setLoading(true);
        try {
            if (mode === 'add') {
                await addAlbum({ ...values });
                message.success("Successfully added the album!");
            } else {
                await updateAlbum(album.id, { ...values });
                message.success("Successfully updated the album!");
            }
            history.push(`/album/view?id=${album.id}`)
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
        {mode === 'edit' && initialState?.currentUser?.id && album?.createdByUserId === initialState?.currentUser?.id && <Button type='primary' icon={<DeleteOutlined />} onClick={handleDeleteClicked}>Delete Album</Button>}
        <Button type='default' icon={<ArrowLeftOutlined />} onClick={handleCancelClicked}>Back</Button>
    </>

    return (
        <PageContainer
            extra={renderActions()}
        >
            <Card >
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
                </ProForm>
            </Card>
        </PageContainer>
    );
};
