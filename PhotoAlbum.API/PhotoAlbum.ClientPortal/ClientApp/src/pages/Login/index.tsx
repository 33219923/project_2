import { LockOutlined, ThunderboltOutlined, UserOutlined } from '@ant-design/icons';
import { Button, message } from 'antd';
import React from 'react';
import { ProFormText, LoginForm } from '@ant-design/pro-form';
import { useIntl, history, FormattedMessage, SelectLang, useModel, Link } from 'umi';
import Footer from '@/components/Footer';
import { login } from '@/services/api';
import styles from './index.less';
import { setToken } from '@/utils/authority';

const Login: React.FC = () => {
  const { initialState, setInitialState } = useModel('@@initialState');

  const intl = useIntl();

  const fetchUserInfo = async () => {

    if (!initialState?.fetchUserInfo) {
      console.log("Function does not exist! initialState?.fetchUserInfo");
    }

    const userInfo = await initialState?.fetchUserInfo?.();

    if (userInfo) {
      await setInitialState((s) => ({
        ...s,
        currentUser: userInfo,
      }));
    }
  };

  const handleSubmit = async (values: API.LoginParams) => {
    try {
      const msg = await login({ ...values });
      if (msg) {
        setToken(msg);
        const defaultLoginSuccessMessage = intl.formatMessage({
          id: 'pages.login.success',
        });
        message.success(defaultLoginSuccessMessage);
        await fetchUserInfo();

        if (!history) return;
        const { query } = history.location;
        const { redirect } = query as { redirect: string };
        history.push(redirect || '/');
        return;
      }
    } catch (error: any) {
      const defaultLoginFailureMessage = error?.message || intl.formatMessage({
        id: 'pages.login.failure',
      });
      message.error(defaultLoginFailureMessage);
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
                <Button type='primary' icon={<ThunderboltOutlined />} style={{ width: '100%', marginTop: 10 }} onClick={() => { props.form?.submit() }}>{intl.formatMessage({ id: 'pages.login.submitText' })}</Button>
              </>
            }
          }}
          logo={<img alt="logo" src="/logo.svg" />}
          title={intl.formatMessage({ id: 'app.title' })}
          subTitle={intl.formatMessage({ id: 'pages.layouts.userLayout.title' })}
          actions={[
            <div
              style={{
                marginBottom: 24,
                textAlign: 'center',
              }}
            >
              <Link to="/user/register">
                <FormattedMessage id="pages.login.registerAccount" />
              </Link>
            </div>,
          ]}
          onFinish={async (values) => {
            await handleSubmit(values as API.LoginParams);
          }}
        >
          <ProFormText
            label='Username'
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
            label='Password'
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
        </LoginForm>
      </div>
      <Footer />
    </div>
  );
};

export default Login;
