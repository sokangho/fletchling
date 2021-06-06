import { faTwitter } from '@fortawesome/free-brands-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

const TwitterAuthButton = () => {
  const onClick = () => {};

  return (
    <button className='flex justify-around p-2 bg-blue-400 w-48 rounded-md'>
      <div>
        <FontAwesomeIcon icon={faTwitter} className='text-white' />
      </div>
      <span>Sign in with Twitter</span>
    </button>
  );
};

export default TwitterAuthButton;
