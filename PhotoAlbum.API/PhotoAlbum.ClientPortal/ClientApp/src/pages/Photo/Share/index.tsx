import React, { useState, useEffect } from 'react';
import { Card, List, message, Modal } from 'antd';
import { listPhotoAvailableUsers, listPhotoSharedUsers, sharePhoto, unsharePhoto } from '@/services/api';
import { ShareAltOutlined, UndoOutlined } from '@ant-design/icons';
import { isArray } from 'lodash';

export default (props: any): JSX.Element => {
    const { photoId, name, onCancel } = props;
    const [availableList, setAvailableList] = useState<any[]>([]);
    const [sharedList, setSharedList] = useState<any[]>([]);
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        refresh();
    }, []);

    const refresh = () => {
        setLoading(true);
        loadAvailableUsers();
        loadSharedUsers();
        setLoading(false);
    }

    const loadAvailableUsers = async () => {
        try {
            const response = await listPhotoAvailableUsers(photoId);
            if (isArray(response)) {
                setAvailableList(response)
            }
        } catch (error) {
            message.error("Unable to load the available users!");
        }
    }

    const loadSharedUsers = async () => {
        try {
            const response = await listPhotoSharedUsers(photoId);
            if (isArray(response)) {
                setSharedList(response)
            }
        } catch (error) {
            message.error("Unable to load the shared users!");
        }
    }

    const handleShareClicked = async (item: any) => {
        try {
            const response = await sharePhoto(photoId, item.id);
            refresh();
        } catch (error) {
            message.error(`Failed to share the photo with ${item.name}!`);
        }
    }

    const handleUnshareClicked = async (item: any) => {
        try {
            const response = await unsharePhoto(photoId, item.id);
            refresh();
        } catch (error) {
            message.error(`Failed to unshare the photo with ${item.name}!`);
        }
    }

    return (
        <Modal visible={true}
            closable={false}
            okButtonProps={{ style: { display: 'none' } }}
            onCancel={onCancel}
            cancelText='Done'
            title={'Share ' + name}
            bodyStyle={{ padding: 0 }}>
            <Card title='Available'
                bodyStyle={{
                    height: 200,
                    overflowY: 'scroll'
                }}>
                <List
                    loading={loading}
                    itemLayout="horizontal"
                    dataSource={availableList}
                    renderItem={item =>
                        <List.Item
                            actions={[
                                <a onClick={() => handleShareClicked(item)}><ShareAltOutlined /> Share</a>
                            ]}
                        >
                            {item.name}
                        </List.Item>
                    }
                    locale={{
                        emptyText: 'No users to share with'
                    }}
                />
            </Card>
            <br />
            <Card title='Shared With'
                bodyStyle={{
                    height: 200,
                    overflowY: 'scroll'
                }}>
                <List
                    itemLayout="horizontal"
                    loading={loading}
                    dataSource={sharedList}
                    renderItem={item =>
                        <List.Item
                            actions={[
                                <a onClick={() => handleUnshareClicked(item)}><UndoOutlined /> Unshare</a>
                            ]}
                        >
                            {item.name}
                        </List.Item>
                    }
                    locale={{
                        emptyText: 'Not shared with anyone'
                    }}
                />
            </Card>
        </Modal>
    );
};
