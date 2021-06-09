import { ReactChild, ReactChildren, useContext } from 'react';
import BeatLoader from 'react-spinners/ClipLoader';
import useSWR from 'swr';

import AuthContext from '@/components/Context/AuthContext';
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
  return (
    <div className='absolute w-full p-4 bg-gray-300 border-1 border-gray-500 '>{children}</div>
  );
};

const SearchResult = ({ username }: Props) => {
  const { currentUser } = useContext(AuthContext);
  const token = currentUser?.token;

  const shouldFetch = username.length > 0 ? true : false;

  const { data, error } = useSWR(
    shouldFetch ? `/twitter/user/search?username=${username}` : null,
    (url) => fetcher(url, token),
    { shouldRetryOnError: false, revalidateOnFocus: false }
  );

  if (!shouldFetch) return null;

  if (error) return <WrappingDiv>failed to load</WrappingDiv>;

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
    <div className='absolute w-full max-h-96 overflow-y-auto overflow-x-hidden'>
      {data.map((user: User, i: number) => (
        <UserResult user={user} key={i} />
      ))}
    </div>
  );
};

export default SearchResult;
