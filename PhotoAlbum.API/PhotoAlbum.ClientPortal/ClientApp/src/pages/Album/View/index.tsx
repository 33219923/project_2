import React, { useState, useEffect } from 'react';
import { PageContainer } from '@ant-design/pro-layout';
import { Button, Card, Form, message, Typography, Image } from 'antd';
import { useHistory, useIntl, useModel } from 'umi';
import styles from './index.less';
import { addAlbum, getAlbum, getPhotosForAlbum, updateAlbum } from '@/services/api';
import ProForm, { ProFormText } from '@ant-design/pro-form';
import { ArrowLeftOutlined, CloseOutlined, FolderOutlined, ShareAltOutlined, ThunderboltOutlined } from '@ant-design/icons';
import ShareAlbumModal from '../Share';
import { convertBlobToUrl } from '@/utils/utils';

const { Title, Text } = Typography;

export default (props: any): React.ReactNode => {
    const { initialState, setInitialState } = useModel('@@initialState');
    const intl = useIntl();
    const [form] = Form.useForm();
    const history = useHistory();

    const [shareModalState, setShareModalState] = useState<any>({ visible: false });
    const [loading, setLoading] = useState<boolean>(true);
    const [loadingPhotos, setLoadingPhotos] = useState<boolean>(true);
    const [album, setAlbum] = useState<any>(undefined);
    const [photos, setPhotos] = useState<any[]>([]);

    useEffect(() => {
        const params = new URLSearchParams(props.location.search);
        const id = params.get('id');

        if (id) {
            loadAlbum(id);
            loadPhotos(id);
        }
        else {
            setLoading(false);
            setLoadingPhotos(false);
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

    const loadPhotos = async (id: string) => {
        try {
            const response = await getPhotosForAlbum(id);
            if (response) setPhotos(response);
        } catch (error) {
            message.error("Unable to load the album photos!");
        }
        setLoadingPhotos(false);
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

    const renderPhoto = (photo: any): JSX.Element => {
        let fileUrl = 'error';
        if (photo?.filename && photo?.data) {
            fileUrl = convertBlobToUrl(photo.data, photo.filename)
        }
        return <Card.Grid
            key={photo.id}
            hoverable
            style={{ width: 230, padding: 15 }}
        >
            <div style={{ textAlign: 'center', padding: 0 }}>
                <Image
                    height={200}
                    src={fileUrl}
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

    const renderNoPhotos = (): JSX.Element => <>NO PHOTOS</>

    return (
        <PageContainer
            extra={renderActions()}
        >
            <Card
                title={<>{album?.name}<br /><Text type='secondary' style={{ fontSize: '0.8em' }}>{album?.description}</Text></>}
                extra={renderCardActions()}
                loading={loading || loadingPhotos}
            >
                {photos.length > 0 && photos.map(photo => renderPhoto(photo))}
                {photos.length === 0 && renderNoPhotos()}
            </Card>
            {shareModalState.visible && <ShareAlbumModal albumId={album?.id} name={album?.name} onCancel={() => { setShareModalState({ visible: false }) }} />}
        </PageContainer >
    );
};
