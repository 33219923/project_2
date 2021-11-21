// @ts-ignore
/* eslint-disable */
import request from '@/utils/request';

export async function register(body: any, options?: { [key: string]: any }) {
  return request<any>(`${API_URL}/api/user`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    data: body,
    ...(options || {}),
  });
}

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

export async function listAlbums(options?: { [key: string]: any }) {
  return request<API.CurrentUser>(`${API_URL}/api/album/list`, {
    method: 'GET',
    ...(options || {}),
  });
}

export async function listSharedAlbums(options?: { [key: string]: any }) {
  return request<API.CurrentUser>(`${API_URL}/api/album/list/shared`, {
    method: 'GET',
    ...(options || {}),
  });
}

export async function listAlbumAvailableUsers(albumId: string, options?: { [key: string]: any }) {
  return request<API.CurrentUser>(`${API_URL}/api/album/list/shared/${albumId}/available`, {
    method: 'GET',
    ...(options || {}),
  });
}

export async function listAlbumSharedUsers(albumId: string, options?: { [key: string]: any }) {
  return request<API.CurrentUser>(`${API_URL}/api/album/list/shared/${albumId}`, {
    method: 'GET',
    ...(options || {}),
  });
}

export async function getAlbum(id: string, options?: { [key: string]: any }) {
  return request<API.CurrentUser>(`${API_URL}/api/album/${id}`, {
    method: 'GET',
    ...(options || {}),
  });
}

export async function addAlbum(body: API.LoginParams, options?: { [key: string]: any }) {
  return request<string>(`${API_URL}/api/album`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    data: body,
    ...(options || {}),
  });
}

export async function updateAlbum(
  id: string,
  body: API.LoginParams,
  options?: { [key: string]: any },
) {
  return request<string>(`${API_URL}/api/album/${id}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
    },
    data: body,
    ...(options || {}),
  });
}

export async function deleteAlbum(id: string, options?: { [key: string]: any }) {
  return request<API.CurrentUser>(`${API_URL}/api/album/${id}`, {
    method: 'DELETE',
    ...(options || {}),
  });
}

export async function shareAlbum(
  albumId: string,
  userId: string,
  options?: { [key: string]: any },
) {
  return request<string>(`${API_URL}/api/album/share`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    data: {
      albumId,
      userId,
    },
    ...(options || {}),
  });
}

export async function unshareAlbum(
  albumId: string,
  userId: string,
  options?: { [key: string]: any },
) {
  return request<string>(`${API_URL}/api/album/unshare`, {
    method: 'DELETE',
    headers: {
      'Content-Type': 'application/json',
    },
    data: {
      albumId,
      userId,
    },
    ...(options || {}),
  });
}

export async function listPhotos(options?: { [key: string]: any }) {
  return request<API.CurrentUser>(`${API_URL}/api/photo/list`, {
    method: 'GET',
    ...(options || {}),
  });
}
