import { useContext, useState } from 'react';
import { TwitterTimelineEmbed } from 'react-twitter-embed';

import HeartIcon from '../HeartIcon';
import TimelineContext from '../TimelineContext';

interface Props {
  username: string;
}

const Timeline = ({ username }: Props) => {
  const [isLoaded, setIsLoaded] = useState(false);
  const { savedTimelines, setSavedTimelines } = useContext(TimelineContext);

  const onTimeLineLoaded = () => {
    console.log('test');
    setIsLoaded(true);
  };

  const removeTimeLine = () => {
    const arr = savedTimelines.filter((a) => a !== username);
    setSavedTimelines(arr);
  };

  return (
    <div>
      {isLoaded && (
        <div className='flex justify-between px-2 bg-twitter-dark'>
          <span className='my-auto text-gray-200'>@{username}</span>
          <span>
            <HeartIcon isSaved={true} onClick={removeTimeLine} />
          </span>
        </div>
      )}
      <TwitterTimelineEmbed
        sourceType='profile'
        screenName={username}
        theme='dark'
        noHeader
        noFooter
        onLoad={onTimeLineLoaded}
        options={{ height: 650, width: 350 }}
      />
    </div>
  );
};

export default Timeline;
