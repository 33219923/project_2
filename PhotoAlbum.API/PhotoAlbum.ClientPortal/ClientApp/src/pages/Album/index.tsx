import React from 'react';
import { PageContainer } from '@ant-design/pro-layout';
import { Card } from 'antd';
import { useIntl } from 'umi';
import styles from './index.less';

export default (props: any): React.ReactNode => {
    const intl = useIntl();

    const params = new URLSearchParams(props.location.search);
    const id = params.get('id');
    console.log("ID=> ", id)

    return (
        <PageContainer>
            <Card>

            </Card>
        </PageContainer>
    );
};
