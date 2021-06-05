import { debounce } from 'lodash';
import { ChangeEvent, useCallback, useEffect, useRef, useState } from 'react';

import SearchBox from '@/components/SearchUser/SearchBox';
import SearchResult from '@/components/SearchUser/SearchResult';

const Search = () => {
  const [shouldRender, setShouldRender] = useState<boolean>(false);
  const [usernameQuery, setUsernameQuery] = useState('');
  const node = useRef<HTMLDivElement>(null);

  useEffect(() => {
    document.addEventListener('mousedown', hideSearchResult);

    return () => {
      document.removeEventListener('mousedown', hideSearchResult);
    };
  }, []);

  useEffect(() => {
    if (usernameQuery.length > 0) {
      setShouldRender(true);
    } else {
      setShouldRender(false);
    }
  }, [usernameQuery]);

  const hideSearchResult = (e: MouseEvent) => {
    if (node.current && !node.current.contains(e.target as Node)) {
      setShouldRender(false);
    }
  };

  const delayedChangeUsername = useCallback(
    debounce((username: string) => setUsernameQuery(username), 1000),
    []
  );

  const autoSearchUser = (e: ChangeEvent<HTMLInputElement>) => {
    delayedChangeUsername(e.target.value);
  };

  return (
    <div ref={node}>
      <SearchBox handleChange={autoSearchUser} />
      {shouldRender && <SearchResult username={usernameQuery} />}
    </div>
  );
};

export default Search;
