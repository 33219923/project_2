export function setToken(token: string): void {
  localStorage.setItem('token', token);
}

export function getToken(): string {
  var tk = localStorage.getItem('token');
  if (tk) {
    return tk as string;
  }

  return '';
}

export function clearToken(): void {
  localStorage.removeItem('token');
}
