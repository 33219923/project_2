import { LockOutlined, UserOutlined } from '@ant-design/icons';
import { Alert, Button, message } from 'antd';
import React, { useState } from 'react';
import { ProFormText, LoginForm } from '@ant-design/pro-form';
import { useIntl, history, FormattedMessage, SelectLang, useModel } from 'umi';
import Footer from '@/components/Footer';
import { login } from '@/services/api';

import styles from './index.less';

const Register: React.FC = () => {
    const intl = useIntl();

    // const handleSubmit = async (values: API.LoginParams) => {
    //     try {
    //         const msg = await login({ ...values });
    //         if (msg) {
    //             //TODO: Set token
    //             const defaultLoginSuccessMessage = intl.formatMessage({
    //                 id: 'pages.login.success',
    //             });
    //             message.success(defaultLoginSuccessMessage);
    //             //await fetchUserInfo();

    //             if (!history) return;
    //             const { query } = history.location;
    //             const { redirect } = query as { redirect: string };
    //             history.push(redirect || '/');
    //             return;
    //         }
    //         console.log(msg);

    //         // setUserLoginState(msg);
    //     } catch (error) {
    //         const defaultLoginFailureMessage = intl.formatMessage({
    //             id: 'pages.login.failure',
    //         });
    //         message.error(defaultLoginFailureMessage);
    //     }
    // };
    return (
        <div className={styles.container}>
            <div className={styles.lang} data-lang>
                {SelectLang && <SelectLang />}
            </div>
            <div className={styles.content}>
                {/* <LoginForm
                    logo={<img alt="logo" src="/logo.svg" />}
                    title={intl.formatMessage({ id: 'app.title' })}
                    subTitle={intl.formatMessage({ id: 'pages.layouts.userLayout.title' })}
                    message="Register"
                    actions={[
                        <div
                            style={{
                                marginBottom: 24,
                                textAlign: 'center',
                            }}
                        >
                            <a>
                                <FormattedMessage id="pages.login.registerAccount" />
                            </a>
                        </div>,
                    ]}
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
                            id: 'pages.login.name.placeholder',
                        })}
                        rules={[
                            {
                                required: true,
                                message: <FormattedMessage id="pages.login.name.required" />,
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
                            id: 'pages.login.surname.placeholder',
                        })}
                        rules={[
                            {
                                required: true,
                                message: <FormattedMessage id="pages.login.name.required" />,
                            },
                        ]}
                    />
                </LoginForm> */}
            </div>
            <Footer />
        </div>
    );
};

export default Register;
