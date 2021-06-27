import axios from 'axios';
import assign from 'lodash/assign';
import { useEffect, useState } from 'react';

import { baseConfig } from '@/lib/axios';

function useFetch<T>(url: string, token?: string) {
  const [data, setData] = useState<T | null>(null);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function init() {
      setLoading(true);

      let config = assign({}, baseConfig);

      if (token) {
        config = assign({ headers: { Authorization: `Bearer ${token}` } }, baseConfig);
      }

      try {
        const res = await axios.get<T>(url, config);
        setData(res.data);
      } catch (e) {
        setError(e);
      } finally {
        setLoading(false);
      }
    }

    if (url) {
      init();
    }
  }, [url, token]);

  return { data, error, loading };
}

export default useFetch;
