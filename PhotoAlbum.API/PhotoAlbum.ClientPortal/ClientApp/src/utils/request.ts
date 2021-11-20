import { extend } from 'umi-request';
import { notification } from 'antd';
import { getToken } from './authority';
import { cloneDeep, merge } from 'lodash';

const codeMessage = {
  200: 'The server successfully returned the requested data.',
  201: 'Create or modify data successfully.',
  202: 'A request has entered the background queue (asynchronous task).',
  204: 'Delete data successfully.',
  400: 'The request was made with an error, and the server did not perform operations to create or modify data.',
  401: 'User does not have permission (token, username, password is incorrect).',
  403: 'User is authorized, but access is forbidden.',
  404: 'The request is made for a record that does not exist, or a resource the server does not have.',
  406: 'The format of the request is not available.',
  410: 'The requested resource is permanently deleted and will not be retrieved.',
  422: 'A validation error occurred when creating an object.',
  500: 'Server error, please check the server.',
  502: 'Gateway error.',
  503: 'Service is unavailable, server is temporarily overloaded or maintained.',
  504: 'Gateway timeout.',
};

const errorHandler = (error: any) => {
  console.log('INTERCEPTED ERROR => ', error.data);

  const accessToken = getToken();
  if (!accessToken) throw error.data;

  if (!error.response) {
    notification.error({
      message: 'Network Error',
      description:
        'There seems to be a problem with the network. Please check your network connection and try again.',
      duration: 15,
    });
    return error.data;
  }

  const { response = {}, data } = error;
  const errortext = response.status ? codeMessage[response.status] : response.statusText;
  const { status, url } = response;

  console.error(data);

  if (status === 401) {
    notification.error({
      message:
        'Authorization error. You are not signed in or your session has expired; please sign out using the `Sign out` menu item on the profile pop-up and then sign back in.',
      duration: 15,
    });
  } else if (status) {
    notification.error({
      message: `Request error ${status}: ${url}`,
      description: errortext,
      duration: 15,
    });
  } else {
    notification.error({
      message: 'Network Error',
      description:
        'There seems to be a problem with the network. Please check your network connection and try again.',
      duration: 15,
    });
  }

  return data;
};

const request = extend({
  errorHandler,
});

request.interceptors.request.use((url, options) => {
  const accessToken = getToken();

  if (accessToken)
    return {
      url,
      options: merge(cloneDeep(options), {
        headers: { Authorization: `Bearer ${accessToken}` },
      }),
    };
  else
    return {
      url,
      options,
    };
});

export default request;
