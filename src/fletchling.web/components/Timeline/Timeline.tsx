interface Props {
  username: string;
}

const Timeline = ({ username }: Props) => {
  const href = `https://twitter.com/${username}?ref_src=twsrc%5Etfw`;

  return (
    <a
      className='twitter-timeline'
      data-width='350'
      data-height='800'
      data-dnt='true'
      data-theme='dark'
      href={href}>
      Tweets by {username}
    </a>
  );
};

export default Timeline;
