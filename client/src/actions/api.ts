import axios from 'axios';

export const apiClient = axios.create({
  baseURL: `https://localhost:44343`,
  responseType: 'json',
  headers: {
    'Access-Control-Allow-Origin': '*',
    'Content-Type': 'application/json',
    'Access-Control-Allow-Credentials': 'true',
  },
});

apiClient.interceptors.request.use(
  config => {
    const token = localStorage.getItem('token');
    config.headers.Authorization = `Bearer ${token}`;
    return config;
  },
  error => new Error(error)
);
