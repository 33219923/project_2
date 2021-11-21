import React, { useState, useEffect } from 'react';
import { PageContainer } from '@ant-design/pro-layout';
import { Button, Card, Form, message, Typography } from 'antd';
import { useHistory, useIntl, useModel } from 'umi';
import styles from './index.less';
import { addAlbum, getAlbum, updateAlbum } from '@/services/api';
import ProForm, { ProFormText } from '@ant-design/pro-form';
import { ArrowLeftOutlined, CloseOutlined, FolderOutlined, ShareAltOutlined, ThunderboltOutlined } from '@ant-design/icons';
import ShareAlbumModal from '../Share';

const { Title, Text } = Typography;

export default (props: any): React.ReactNode => {
    const { initialState, setInitialState } = useModel('@@initialState');
    const intl = useIntl();
    const [form] = Form.useForm();
    const history = useHistory();

    const [shareModalState, setShareModalState] = useState<any>({ visible: false });
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

    const handleShareClicked = () => {
        setShareModalState({ visible: true })
    }


    const renderActions = (): JSX.Element => <>
        {initialState?.currentUser?.id && album?.createdByUserId === initialState?.currentUser?.id && <Button type='primary' icon={<FolderOutlined />} onClick={handleEditClicked} >Edit Album</Button>}
        <Button type='default' icon={<ArrowLeftOutlined />} onClick={() => history.push(`/albums`)}>Back</Button>
    </>

    const renderCardActions = (): JSX.Element => <>
        {initialState?.currentUser?.id && album?.createdByUserId === initialState?.currentUser?.id && <Button type='primary' icon={<ShareAltOutlined />} onClick={handleShareClicked} >Share Album</Button>}
    </>

    return (
        <PageContainer
            extra={renderActions()}
        >
            <Card
                title={<>{album?.name}<br /><Text type='secondary' style={{ fontSize: '0.8em' }}>{album?.description}</Text></>}
                extra={renderCardActions()}
            >
            </Card>
            {shareModalState.visible && <ShareAlbumModal albumId={album?.id} name={album?.name} onCancel={() => { setShareModalState({ visible: false }) }} />}
        </PageContainer >
    );
};
