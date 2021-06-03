import React, { useEffect, useRef, useState } from 'react';

import SearchResult from '@/components/SearchUser/SearchResult';
import useForm from '@/lib/useForm';

const SearchBox = () => {
  const initialState = { username: '' };

  const [shouldRender, setShouldRender] = useState<boolean>(false);
  const { values, handleChange } = useForm(initialState);
  const node = useRef<HTMLDivElement>(null);

  useEffect(() => {
    document.addEventListener('mousedown', hideSearchResult);

    return () => {
      document.removeEventListener('mousedown', hideSearchResult);
    };
  }, []);

  const hideSearchResult = (e: MouseEvent) => {
    if (node.current && !node.current.contains(e.target as Node)) {
      setShouldRender(false);
    }
  };

  const searchUser = () => {
    setShouldRender(true);
  };

  return (
    <div ref={node}>
      <div className='flex'>
        <input
          type='text'
          name='username'
          value={values.username}
          onChange={handleChange}
          placeholder='Search...'
          className='flex-auto w-10/12 bg-gray-300 border-2 border-black p-3'
        />
        <button onClick={searchUser} className='flex-auto bg-blue-500 rounded'>
          Search
        </button>
      </div>
      {shouldRender && <SearchResult username={values.username as string} />}
    </div>
  );
};

export default SearchBox;
