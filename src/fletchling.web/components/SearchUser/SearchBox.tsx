import React, { ChangeEvent } from 'react';

interface Props {
  handleChange: (e: ChangeEvent<HTMLInputElement>) => void;
}

const SearchBox = ({ handleChange }: Props) => {
  return (
    <input
      type='text'
      name='username'
      onChange={handleChange}
      placeholder='Search...'
      className='bg-gray-300 border-2 border-black p-3 w-80'
    />
  );
};

export default SearchBox;
