import { faTwitter } from '@fortawesome/free-brands-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

import { twitterLogIn } from '@/lib/firebase';

const TwitterAuthButton = () => {
  const onClick = async () => {
    await twitterLogIn();
  };

  return (
    <button className='flex-none h-8 bg-blue-400 rounded-2xl' onClick={async () => await onClick()}>
      <div className='flex gap-x-3 px-3'>
        <div>
          <FontAwesomeIcon icon={faTwitter} className='text-white' />
        </div>
        Twitter Log In
      </div>
    </button>
  );
};

export default TwitterAuthButton;
