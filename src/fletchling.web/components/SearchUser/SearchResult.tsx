import { ReactChild, ReactChildren } from 'react';
import BeatLoader from 'react-spinners/ClipLoader';
import useSWR from 'swr';

import UserResult from '@/components/SearchUser/UserResult';
import User from '@/interfaces/User';
import { fetcher } from '@/lib/axios';

const BeatLoaderCss = `
  display: block;
  margin: 0 auto;
  size: 10px;
`;

interface Props {
  username: string;
}

interface WrappingDivProps {
  children: ReactChild | ReactChildren;
}

const WrappingDiv = ({ children }: WrappingDivProps) => {
  return <div className='p-4 bg-gray-300 border-1 border-gray-500 '>{children}</div>;
};

const SearchResult = ({ username }: Props) => {
  const shouldFetch = username.length > 0 ? true : false;
  const { data, error } = useSWR(shouldFetch ? `/twitter/?username=${username}` : null, fetcher);

  if (error) return <div>failed to load</div>;

  // Loading
  if (!data) {
    return (
      <WrappingDiv>
        <BeatLoader loading={true} css={BeatLoaderCss} size={30} color='grey' />
      </WrappingDiv>
    );
  }

  // No data
  if (data.length === 0) {
    return <WrappingDiv>No users found</WrappingDiv>;
  }

  return (
    <div className='h-80 overflow-y-auto'>
      {data.map((user: User, i: number) => (
        <UserResult user={user} key={i} />
      ))}
    </div>
  );
};

export default SearchResult;
