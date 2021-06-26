import TwitterAuthButton from '@/components/Buttons/TwitterAuthButton';
import Search from '@/components/SearchUser/Search';

const NavBar = () => {
  return (
    <div className='flex gap-3 items-center p-1 bg-primary-dark'>
      <Search />
      <TwitterAuthButton />
    </div>
  );
};

export default NavBar;
