import TwitterAuthButton from '@/components/TwitterAuthButton';

const NavBar = () => {
  return (
    <div className='flex justify-end'>
      <TwitterAuthButton />
    </div>
  );
};

export default NavBar;
