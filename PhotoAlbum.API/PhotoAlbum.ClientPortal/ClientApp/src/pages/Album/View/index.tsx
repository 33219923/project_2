import React, { useState, useEffect } from 'react';
import { PageContainer } from '@ant-design/pro-layout';
import { Button, Card, Form, message, Typography } from 'antd';
import { useHistory, useIntl } from 'umi';
import styles from './index.less';
import { addAlbum, getAlbum, updateAlbum } from '@/services/api';
import ProForm, { ProFormText } from '@ant-design/pro-form';
import { CloseOutlined, FolderOutlined, ThunderboltOutlined } from '@ant-design/icons';

const { Title, Text } = Typography;

export default (props: any): React.ReactNode => {
    const intl = useIntl();
    const [form] = Form.useForm();
    const history = useHistory();

    const [loading, setLoading] = useState<boolean>(true);
    const [album, setAlbum] = useState<any>(undefined);

    useEffect(() => {
        const params = new URLSearchParams(props.location.search);
        const id = params.get('id');

        if (id) {
            loadAlbum(id);
        }
        else {
            setLoading(false);
        }
    }, []);

    const loadAlbum = async (id: string) => {
        try {
            const response = await getAlbum(id);
            if (response) setAlbum(response);
        } catch (error) {
            message.error("Unable to load the album!");
        }
        setLoading(false);
    }

    const handleEditClicked = () => {
        history.push(`/album/edit?id=${album.id}`)
    }

    const renderActions = (): JSX.Element => <>
        <Button type='primary' icon={<FolderOutlined />} onClick={handleEditClicked} >Edit Album</Button>
    </>

    return (
        <PageContainer
            extra={renderActions()}
        >
            <Card title={<>{album?.name}<br /><Text type='secondary' style={{ fontSize: '0.8em' }}>{album?.description}</Text></>}>

            </Card>
        </PageContainer >
    );
};
