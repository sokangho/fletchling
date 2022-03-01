import { faTwitter } from '@fortawesome/free-brands-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { signIn } from 'next-auth/client';

const TwitterSignInButton = () => {
  return (
    <button
      className='flex items-center h-12 p-7 bg-blue-400 rounded-2xl'
      onClick={() => signIn('twitter')}>
      <div className='flex gap-x-3 text-xl'>
        <span>
          <FontAwesomeIcon icon={faTwitter} className='text-white' />
        </span>
        <span>Sign in with Twitter</span>
      </div>
    </button>
  );
};

export default TwitterSignInButton;
