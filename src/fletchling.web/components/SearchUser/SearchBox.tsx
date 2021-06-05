import React, { ChangeEvent, MouseEvent } from 'react';

interface Props {
  handleChange: (e: ChangeEvent<HTMLInputElement>) => void;
  onClick: (e: MouseEvent<HTMLInputElement>) => void;
}

const SearchBox = ({ handleChange, onClick }: Props) => {
  return (
    <input
      type='text'
      name='username'
      onChange={handleChange}
      onClick={onClick}
      autoComplete='off'
      placeholder='Search for Twitter user...'
      className='bg-gray-300 border-2 border-black p-3 w-full'
    />
  );
};

export default SearchBox;
