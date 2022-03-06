import { useSession } from 'next-auth/client';
import { ReactChild, ReactChildren } from 'react';
import { useQuery } from 'react-query';
import ClipLoader from 'react-spinners/ClipLoader';
import apiService from 'services/apiService';

import SearchResult from '@/components/SearchUser/SearchResult';
import TwitterUser from '@/interfaces/TwitterUser';

const ClipLoaderCss = `
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
  return <div className='absolute w-full mt-1 p-4 bg-gray-600 text-gray-400'>{children}</div>;
};

const SearchResultList = ({ username }: Props) => {
  const [session, sessionLoading] = useSession();

  if (sessionLoading) {
    return (
      <WrappingDiv>
        <ClipLoader loading={true} css={ClipLoaderCss} size={30} color='grey' />
      </WrappingDiv>
    );
  }

  const { data, error, isLoading } = useQuery('searchUsers', async () => {
    const users = await apiService.searchTwitterUser({ username, jwt: session?.jwt as string });
    return users;
  });

  if (error) return <WrappingDiv>failed to load</WrappingDiv>;

  // Loading
  if (isLoading) {
    return (
      <WrappingDiv>
        <ClipLoader loading={true} css={ClipLoaderCss} size={30} color='grey' />
      </WrappingDiv>
    );
  }

  // No data
  if (data?.length === 0) {
    return <WrappingDiv>No users found</WrappingDiv>;
  }

  return (
    <div className='absolute mt-1 w-full max-h-96 overflow-y-auto overflow-x-hidden divide-y divide-gray-500'>
      {data?.map((user: TwitterUser, i: number) => (
        <SearchResult user={user} key={i} />
      ))}
    </div>
  );
};

export default SearchResultList;
