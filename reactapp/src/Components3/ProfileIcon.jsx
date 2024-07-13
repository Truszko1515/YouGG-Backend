import React, { useEffect, useState, Suspense } from "react";


export default function ProfileIcon({ summonerName }) {
  const [summonerInfo, setSummonerInfo] = useState();
  const [isLoaded, setIsLoaded] = useState(false)

  useEffect(() => {
        const fetchData = async () => {
          const response = await fetch("https://localhost:7041/api/summoner/" + summonerName);
          const data = await response.json();
          setSummonerInfo(data);
          setIsLoaded(true);
        }

        fetchData();
  }, [])

    return <div>{isLoaded ? summonerInfo.profileIconId : "niezaladowane"}</div>
}
