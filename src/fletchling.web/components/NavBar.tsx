import { useContext } from 'react';

import TwitterAuthButton from '@/components/Buttons/TwitterAuthButton';
import GlobalStateContext from '@/components/Context/GlobalStateContext';
import Search from '@/components/SearchUser/Search';

import UserProfile from './UserProfile';

const NavBar = () => {
  const {
    globalState: { currentUser }
  } = useContext(GlobalStateContext);

  return (
    <div className='flex gap-3 items-center p-1 bg-primary-dark'>
      <Search />
      {currentUser && <UserProfile user={currentUser.twitterUser} />}
      {!currentUser && <TwitterAuthButton />}
    </div>
  );
};

export default NavBar;
