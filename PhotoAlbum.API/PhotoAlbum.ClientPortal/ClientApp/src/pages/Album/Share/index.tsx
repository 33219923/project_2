import React, { useState, useEffect } from 'react';

import { Card, List, message, Modal } from 'antd';
import { listAlbumAvailableUsers, listAlbumSharedUsers, shareAlbum, unshareAlbum } from '@/services/api';
import { ShareAltOutlined, UndoOutlined } from '@ant-design/icons';
import { isArray } from 'lodash';

export default (props: any): JSX.Element => {
    const { albumId, name, onCancel } = props;
    const [availableList, setAvailableList] = useState<any[]>([]);
    const [sharedList, setSharedList] = useState<any[]>([]);
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        refresh();
    }, []);

    useEffect(() => {
        console.log(availableList, sharedList);
    }, [availableList, sharedList]);

    const refresh = () => {
        setLoading(true);
        loadAvailableUsers();
        loadSharedUsers();
        setLoading(false);
    }

    const loadAvailableUsers = async () => {
        try {
            const response = await listAlbumAvailableUsers(albumId);
            if (isArray(response)) {
                setAvailableList(response)
            }
        } catch (error) {
            message.error("Unable to load the available users!");
        }
    }

    const loadSharedUsers = async () => {
        try {
            const response = await listAlbumSharedUsers(albumId);
            if (isArray(response)) {
                setSharedList(response)
            }
        } catch (error) {
            message.error("Unable to load the shared users!");
        }
    }

    const handleShareClicked = async (item: any) => {
        try {
            const response = await shareAlbum(albumId, item.id);
            refresh();
        } catch (error) {
            message.error(`Failed to share the album with ${item.name}!`);
        }
    }

    const handleUnshareClicked = async (item: any) => {
        try {
            const response = await unshareAlbum(albumId, item.id);
            refresh();
        } catch (error) {
            message.error(`Failed to unshare the album with ${item.name}!`);
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
