import { signOut, useSession } from 'next-auth/client';

import TwitterAuthButton from '@/components/Buttons/TwitterAuthButton';
import Logo from '@/components/Logo';
import Search from '@/components/SearchUser/Search';
import TwitterUser from '@/interfaces/TwitterUser';

import UserProfileMenu from './UserProfileMenu/UserProfileMenu';

const NavBar = () => {
  const [session] = useSession();

  return (
    <div className='flex gap-3 justify-between items-center p-2 bg-twitter-dark'>
      <Logo />
      <Search />
      {session && <UserProfileMenu user={session.twitterUser as TwitterUser} />}
      {!session && <TwitterAuthButton />}
    </div>
  );
};

export default NavBar;
