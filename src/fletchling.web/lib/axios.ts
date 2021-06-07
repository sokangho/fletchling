import axios, { AxiosRequestConfig } from 'axios';
import assign from 'lodash/assign';

const baseConfig: AxiosRequestConfig = {
  baseURL: 'https://localhost:5001/api',
  timeout: 10000 // 10 seconds,
};

const fetcher = (url: string, token: string | undefined) => {
  let config = assign({}, baseConfig);

  if (token) {
    config = assign({ headers: { Authorization: `Bearer ${token}` } }, baseConfig);
  }

  return axios.get(url, config).then((res) => res.data);
};

export { fetcher };
