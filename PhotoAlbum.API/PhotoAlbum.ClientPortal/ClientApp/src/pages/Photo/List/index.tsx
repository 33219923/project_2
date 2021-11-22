import React, { useEffect, useState } from 'react';
import { PageContainer } from '@ant-design/pro-layout';
import { Button, Card, Image, Input, message, Tag, Typography } from 'antd';
import { history } from 'umi';
import { DeleteOutlined, SearchOutlined, VideoCameraAddOutlined } from '@ant-design/icons';
import { listPhotos, listSharedPhotos } from '@/services/api';
import { debounce, isArray } from 'lodash';
import { convertBlobToUrl, PresetColorType } from '@/utils/utils';
import { ProFormText } from '@ant-design/pro-form';

export default (): React.ReactNode => {
    const [photos, setPhotos] = useState<any[]>([]);
    const [sharedPhotos, setSharedPhotos] = useState<any[]>([]);

    const [loadingPhotos, setLoadingPhotos] = useState<boolean>(true);
    const [loadingShared, setLoadingShared] = useState<boolean>(true);

    useEffect(() => {
        refreshPhotos();
        refreshSharedPhotos();
    }, []);

    const refreshPhotos = async (searchString: any = undefined) => {
        try {
            const response = await listPhotos(searchString);
            if (isArray(response)) {
                setPhotos(response)
            }
        } catch (error) {
            message.error("Unable to refresh the photos!");
        }
        setLoadingPhotos(false);
    }

    const refreshSharedPhotos = async (searchString: any = undefined) => {
        try {
            const response = await listSharedPhotos(searchString);
            if (isArray(response)) {
                setSharedPhotos(response)
            }
        } catch (error) {
            message.error("Unable to refresh the shared albums!");
        }
        setLoadingShared(false);
    }

    const renderNoPhotos = (): JSX.Element => <>NO PHOTOS</>

    const handleAddClicked = () => {
        history.push('/photo/add');
    }

    const handleViewClicked = (id: string) => {
        history.push(`/photo/view?id=${id}`);
    }

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
            <div onClick={() => handleViewClicked(photo?.id)} style={{ textAlign: 'center', padding: 0 }}>
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

    const renderActions = (): JSX.Element => <>
        <Button type='primary' icon={<VideoCameraAddOutlined />} onClick={handleAddClicked} >Add Photo</Button>
    </>

    const handleSearch = debounce((value: any) => {
        refreshPhotos(value);
        refreshSharedPhotos(value);
    }, 500)

    const renderSearch = (): JSX.Element => <>
        <ProFormText
            fieldProps={{
                prefix: <SearchOutlined />,
                onChange: (e) => {
                    handleSearch(e?.target?.value)
                }
            }}
            placeholder='Search photo metadata'
        //addonAfter={<Button type={'primary'} onClick={handleSearch} >Search</Button>}
        />
    </>

    return (
        <PageContainer
            title={'Photos'}
            extra={renderActions()}
        >
            {renderSearch()}
            <Card title='My Photos' loading={loadingPhotos}>
                {photos.length > 0 && photos.map(photo => renderPhoto(photo))}
                {photos.length === 0 && renderNoPhotos()}
            </Card>
            <br />
            <Card title='Photos Shared With Me' loading={loadingShared}>
                {sharedPhotos.length > 0 && sharedPhotos.map(photo => renderPhoto(photo))}
                {sharedPhotos.length === 0 && renderNoPhotos()}
            </Card>
        </PageContainer>
    );
};
