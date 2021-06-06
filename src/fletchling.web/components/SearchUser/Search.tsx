import { debounce } from 'lodash';
import { ChangeEvent, useCallback, useEffect, useRef, useState } from 'react';

import SearchBox from '@/components/SearchUser/SearchBox';
import SearchResult from '@/components/SearchUser/SearchResult';

const Search = () => {
  const [shouldRender, setShouldRender] = useState<boolean>(false);
  const [usernameQuery, setUsernameQuery] = useState('');
  const node = useRef<HTMLDivElement>(null);

  // Register event listeners
  useEffect(() => {
    document.addEventListener('mousedown', hideSearchResult);

    return () => {
      document.removeEventListener('mousedown', hideSearchResult);
    };
  }, []);

  // Render SearchResult when usernameQuery is not empty
  useEffect(() => {
    if (usernameQuery.length > 0) {
      setShouldRender(true);
    } else {
      setShouldRender(false);
    }
  }, [usernameQuery]);

  // Hide SearchResult when click outside of Search area
  const hideSearchResult = (e: MouseEvent) => {
    if (node.current && !node.current.contains(e.target as Node)) {
      setShouldRender(false);
    }
  };

  // Only change usernameQuery after user has stopped typing for 1 second
  const delayedChangeUsername = useCallback(
    debounce((username: string) => setUsernameQuery(username), 800),
    []
  );

  const autoSearchUser = (e: ChangeEvent<HTMLInputElement>) => {
    delayedChangeUsername(e.target.value);
  };

  const showSearchResult = () => {
    setShouldRender(true);
  };

  return (
    <div ref={node} className='w-96 relative mx-auto my-5'>
      <SearchBox handleChange={autoSearchUser} onClick={showSearchResult} />
      {shouldRender && <SearchResult username={usernameQuery} />}
    </div>
  );
};

export default Search;
