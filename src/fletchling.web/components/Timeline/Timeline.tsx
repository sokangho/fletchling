import { TwitterTimelineEmbed } from 'react-twitter-embed';

interface Props {
  username: string;
}

const Timeline = ({ username }: Props) => {
  return (
    <TwitterTimelineEmbed
      sourceType='profile'
      screenName={username}
      theme='dark'
      options={{ height: 400, width: 350 }}
    />
  );
};

export default Timeline;
