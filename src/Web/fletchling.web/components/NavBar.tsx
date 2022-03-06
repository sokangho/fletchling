import { useSession } from 'next-auth/client';

import Logo from '@/components/Logo';
import SearchUser from '@/components/SearchUser/';
import TwitterUser from '@/interfaces/TwitterUser';

import UserProfileMenu from './UserProfileMenu/UserProfileMenu';

const NavBar = () => {
  const [session] = useSession();

  return (
    <div className='flex gap-3 justify-between items-center p-2 bg-twitter-dark'>
      <Logo />
      <SearchUser />
      {session && <UserProfileMenu user={session.twitterUser as TwitterUser} />}
    </div>
  );
};

export default NavBar;
