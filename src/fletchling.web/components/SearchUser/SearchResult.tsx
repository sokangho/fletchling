import useSWR from 'swr';

import UserItem from '@/components/SearchUser/UserItem';
import User from '@/interfaces/User';
import { fetcher } from '@/lib/axios';

interface Props {
  username: string;
}

const SearchResult = ({ username }: Props) => {
  const { data, error } = useSWR(`/twitter/?username=${username}`, fetcher);

  if (error) return <div>failed to load</div>;
  if (!data) return <div>Loading...</div>;

  return (
    <div>
      {data.map((user: User) => (
        <UserItem user={user} key={user.id.toString()} />
      ))}
    </div>
  );
};

export default SearchResult;
