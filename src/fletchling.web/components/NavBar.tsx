import TwitterAuthButton from '@/components/Buttons/TwitterAuthButton';

const NavBar = () => {
  return (
    <div className='flex justify-end'>
      <TwitterAuthButton />
    </div>
  );
};

export default NavBar;
