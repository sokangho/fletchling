import { useContext } from 'react';

import TwitterAuthButton from '@/components/Buttons/TwitterAuthButton';
import GlobalStateContext from '@/components/Context/GlobalStateContext';
import Logo from '@/components/Logo';
import Search from '@/components/SearchUser/Search';

import UserProfileMenu from './UserProfileMenu/UserProfileMenu';

const NavBar = () => {
  const {
    globalState: { currentUser }
  } = useContext(GlobalStateContext);

  return (
    <div className='flex gap-3 justify-between items-center p-2 bg-twitter-dark'>
      <Logo />
      <Search />
      {currentUser && <UserProfileMenu user={currentUser.twitterUser} />}
      {!currentUser && <TwitterAuthButton />}
    </div>
  );
};

export default NavBar;
