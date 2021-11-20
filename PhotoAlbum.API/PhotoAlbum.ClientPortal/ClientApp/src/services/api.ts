// @ts-ignore
/* eslint-disable */
import request from '@/utils/request';

export async function currentUser(options?: { [key: string]: any }) {
  return request<API.CurrentUser>(`${API_URL}/api/user/currentUser`, {
    method: 'GET',
    ...(options || {}),
  });
}

export async function login(body: API.LoginParams, options?: { [key: string]: any }) {
  return request<string>(`${API_URL}/api/user/token`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    data: body,
    ...(options || {}),
  });
}
