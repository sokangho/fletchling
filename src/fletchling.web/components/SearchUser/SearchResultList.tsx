import { ReactChild, ReactChildren, useContext } from 'react';
import ClipLoader from 'react-spinners/ClipLoader';
import useSWR from 'swr';

import GlobalStateContext from '@/components/Context/GlobalStateContext';
import SearchResult from '@/components/SearchUser/SearchResult';
import TwitterUser from '@/interfaces/TwitterUser';
import { fetcher } from '@/lib/axios';

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
  return (
    <div className='absolute w-full p-4 bg-gray-300 border-1 border-gray-500 '>{children}</div>
  );
};

const SearchResultList = ({ username }: Props) => {
  const {
    globalState: { currentUser }
  } = useContext(GlobalStateContext);
  const token = currentUser?.token;

  const shouldFetch = username.length > 0 ? true : false;

  const { data, error } = useSWR(
    shouldFetch ? `/twitter/user/search?username=${username}` : null,
    (url) => fetcher<TwitterUser[]>(url, token),
    { shouldRetryOnError: false, revalidateOnFocus: false }
  );

  if (!shouldFetch) return null;

  if (error) return <WrappingDiv>failed to load</WrappingDiv>;

  // Loading
  if (!data) {
    return (
      <WrappingDiv>
        <ClipLoader loading={true} css={ClipLoaderCss} size={30} color='grey' />
      </WrappingDiv>
    );
  }

  // No data
  if (data.length === 0) {
    return <WrappingDiv>No users found</WrappingDiv>;
  }

  return (
    <div className='absolute w-full max-h-96 overflow-y-auto overflow-x-hidden'>
      {data.map((user: TwitterUser, i: number) => (
        <SearchResult user={user} key={i} />
      ))}
    </div>
  );
};

export default SearchResultList;
