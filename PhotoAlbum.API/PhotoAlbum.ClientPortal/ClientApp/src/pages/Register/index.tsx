import { CloseOutlined, LockOutlined, ThunderboltOutlined, UserOutlined } from '@ant-design/icons';
import { Button, message } from 'antd';
import React from 'react';
import { ProFormText, LoginForm } from '@ant-design/pro-form';
import { useIntl, history, FormattedMessage, SelectLang } from 'umi';
import Footer from '@/components/Footer';
import { register } from '@/services/api';

import styles from './index.less';

const Register: React.FC = () => {
    const intl = useIntl();

    const handleCancel = () => {
        if (!history) return;
        const { query } = history.location;
        const { redirect } = query as { redirect: string };
        history.push(redirect || '/');
        return;
    }
    const handleSubmit = async (values: API.LoginParams) => {
        try {
            const msg = await register({ ...values });
            if (msg) {
                const defaultRegisterSuccessMessage = intl.formatMessage({
                    id: 'pages.register.success',
                });
                message.success(defaultRegisterSuccessMessage);
                handleCancel();
            }
        } catch (error: any) {
            const defaultRegisterFailureMessage = error?.message || intl.formatMessage({
                id: 'pages.register.failure',
            });
            message.error(defaultRegisterFailureMessage);
        }
    };
    return (
        <div className={styles.container}>
            <div className={styles.lang} data-lang>
                {SelectLang && <SelectLang />}
            </div>
            <div className={styles.content}>
                <LoginForm
                    submitter={{
                        render: (props, dom) => {
                            return <>
                                <Button type='primary' icon={<ThunderboltOutlined />} style={{ width: '100%', marginTop: 10 }} onClick={() => { props.form?.submit() }}>Register</Button>
                                <Button type='default' icon={<CloseOutlined />} style={{ width: '100%', marginTop: 10 }} onClick={handleCancel}>Cancel</Button>
                            </>
                        }
                    }}
                    logo={<img alt="logo" src="/logo.svg" />}
                    title={intl.formatMessage({ id: 'app.title' })}
                    subTitle={intl.formatMessage({ id: 'pages.layouts.userLayout.title' })}
                    onFinish={async (values) => {
                        await handleSubmit(values as API.LoginParams);
                    }}
                >
                    <ProFormText
                        name="username"
                        fieldProps={{
                            size: 'large',
                            prefix: <UserOutlined className={styles.prefixIcon} />,
                        }}
                        placeholder={intl.formatMessage({
                            id: 'pages.login.username.placeholder',
                        })}
                        rules={[
                            {
                                required: true,
                                message: <FormattedMessage id="pages.login.username.required" />,
                            },
                        ]}
                    />
                    <ProFormText.Password
                        name="password"
                        fieldProps={{
                            size: 'large',
                            prefix: <LockOutlined className={styles.prefixIcon} />,
                        }}
                        placeholder={intl.formatMessage({
                            id: 'pages.login.password.placeholder',
                        })}
                        rules={[
                            {
                                required: true,
                                message: <FormattedMessage id="pages.login.password.required" />,
                            },
                        ]}
                    />
                    <ProFormText
                        name="name"
                        fieldProps={{
                            size: 'large',
                            prefix: <UserOutlined className={styles.prefixIcon} />,
                        }}
                        placeholder={intl.formatMessage({
                            id: 'pages.register.name.placeholder',
                        })}
                        rules={[
                            {
                                required: true,
                                message: <FormattedMessage id="pages.register.name.required" />,
                            },
                        ]}
                    />
                    <ProFormText
                        name="surname"
                        fieldProps={{
                            size: 'large',
                            prefix: <UserOutlined className={styles.prefixIcon} />,
                        }}
                        placeholder={intl.formatMessage({
                            id: 'pages.register.surname.placeholder',
                        })}
                        rules={[
                            {
                                required: true,
                                message: <FormattedMessage id="pages.register.name.required" />,
                            },
                        ]}
                    />
                </LoginForm>
            </div>
            <Footer />
        </div>
    );
};

export default Register;