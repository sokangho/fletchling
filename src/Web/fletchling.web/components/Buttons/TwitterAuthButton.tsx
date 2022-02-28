import { faTwitter } from '@fortawesome/free-brands-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { signIn } from 'next-auth/client';

const TwitterAuthButton = () => {
  return (
    <button className='flex-none h-8 bg-blue-400 rounded-2xl' onClick={() => signIn('twitter')}>
      <div className='flex gap-x-3 px-3'>
        <div>
          <FontAwesomeIcon icon={faTwitter} className='text-white' />
        </div>
        Sign in with Twitter
      </div>
    </button>
  );
};

export default TwitterAuthButton;
