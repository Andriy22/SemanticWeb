import React, {useEffect, useState} from 'react';
import {useParams} from "react-router-dom";
import {ScientistFull} from "@/types/ScientistFull.ts";
import {getScientist} from "@/services/api.ts";

const Home = () => {
    const [data, setData] = useState<ScientistFull | null>(null)

    const {wikiId} = useParams();

    useEffect(() => {
        getScientist(wikiId).then((res) => {
            setData(res)
        }).catch(() => {
            setData(null)
        });

    }, [wikiId])

    if (!data) {
        return <div>Not found</div>
    }


    return (
        <div>
            <div className={'flex justify-center mt-10'}>
                <div className={'md:w-1/2 w-9/12'}>
                    <div className='grid gap-4 md:grid-cols-2 grid-cols-1'>
                        <div className="grid grid-cols-2 gap-10 text-xl">
                            <img className="rounded-t-lg h-80 w-full"
                                 onError={() => setData({
                                     ...data!,
                                     imageUrl: 'https://icons.veryicon.com/png/o/internet--web/web-interface-flat/6606-male-user.png'
                                 })}
                                 src={data?.imageUrl}
                                 alt=""/>

                            <div>
                                <p>Ім'я: {data.name}</p>
                                <p>Дата народження: {data.birthDate} (<a href={data.birthPlaceUrl}
                                                                         target='_blank'>{data.birthPlace}</a>)</p>
                                <p>Заняття: {data.occupation}</p>
                            </div>
                        </div>


                    </div>
                    <div className="mt-10">
                        <p>{data.description}</p>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Home;