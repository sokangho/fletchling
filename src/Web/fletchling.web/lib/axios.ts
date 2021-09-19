import axios, { AxiosRequestConfig } from 'axios';
import assign from 'lodash/assign';

const baseConfig: AxiosRequestConfig = {
  baseURL: process.env.NEXT_PUBLIC_API_BASE_URL,
  timeout: 10000 // 10 seconds,
};

const instance = axios.create(baseConfig);

async function fetcher<T>(url: string, token: string | undefined) {
  let config = assign({}, baseConfig);

  if (token) {
    config = assign({ headers: { Authorization: `Bearer ${token}` } }, baseConfig);
  }

  return axios.get<T>(url, config).then((res) => res.data);
}

async function patcher<T>(url: string, data: T, token: string | undefined) {
  let config = assign({}, baseConfig);

  if (token) {
    config = assign({ headers: { Authorization: `Bearer ${token}` } }, baseConfig);
  }

  return axios.patch(url, data, config).then((res) => res.data);
}

export default instance;
export { baseConfig, fetcher, patcher };
