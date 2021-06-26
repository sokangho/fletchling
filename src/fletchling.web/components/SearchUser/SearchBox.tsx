import { faSearch } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { ChangeEvent, MouseEvent } from 'react';

interface Props {
  handleChange: (e: ChangeEvent<HTMLInputElement>) => void;
  onClick: (e: MouseEvent<HTMLInputElement>) => void;
}

const SearchBox = ({ handleChange, onClick }: Props) => {
  return (
    <div className='flex w-full bg-gray-600 rounded-md'>
      <div className='px-4 py-2 text-lg text-gray-400'>
        <FontAwesomeIcon icon={faSearch} />
      </div>
      <input
        type='text'
        name='username'
        onChange={handleChange}
        onClick={onClick}
        autoComplete='off'
        placeholder='Search for Twitter user...'
        className='w-full bg-gray-600 px-2 py-1 border-0 rounded-md text-gray-200 focus:outline-none'
      />
    </div>
  );
};

export default SearchBox;
