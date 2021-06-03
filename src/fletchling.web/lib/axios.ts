import axios from 'axios';

const instance = axios.create({
  baseURL: 'https://localhost:5001/api',
  timeout: 10000 // 10 seconds
});

const fetcher = (url: string) => instance.get(url).then((res) => res.data);

export default instance;
export { fetcher };
