import { useIntl } from 'umi';
import { GithubOutlined } from '@ant-design/icons';
import { DefaultFooter } from '@ant-design/pro-layout';

export default () => {
  const intl = useIntl();
  const defaultMessage = intl.formatMessage({
    id: 'app.copyright.produced',
    defaultMessage: '蚂蚁集团体验技术部出品',
  });

  const currentYear = new Date().getFullYear();

  return (
    <DefaultFooter
      copyright={`${currentYear} ${defaultMessage}`}
      links={[
        {
          key: 'github',
          title: <><GithubOutlined /> View project on GitHub</>,
          href: 'https://github.com/33219923/project_2',
          blankTarget: true,
        }
      ]}
    />
  );
};
