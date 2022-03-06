import { debounce } from 'lodash';
import { ChangeEvent, useCallback, useEffect, useRef, useState } from 'react';

import SearchBox from '@/components/SearchUser/SearchBox';
import SearchResultList from '@/components/SearchUser/SearchResultList';

const SearchUser = () => {
  const [shouldRender, setShouldRender] = useState<boolean>(false);
  const [username, setUsername] = useState('');
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
      setUsername('');
      setUsernameQuery('');
      setShouldRender(false);
    }
  };

  // Only change usernameQuery after user has stopped typing for 1 second
  const delayedChangeUsername = useCallback(
    debounce((username: string) => setUsernameQuery(username), 800),
    []
  );

  const autoSearchUser = (e: ChangeEvent<HTMLInputElement>) => {
    setUsername(e.target.value);
    delayedChangeUsername(e.target.value);
  };

  const showSearchResult = () => {
    setShouldRender(true);
  };

  return (
    <div ref={node} className='flex-1 relative min-w-80 max-w-2xl'>
      <SearchBox value={username} handleChange={autoSearchUser} onClick={showSearchResult} />
      {shouldRender && usernameQuery !== '' && <SearchResultList username={usernameQuery} />}
    </div>
  );
};

export default SearchUser;
