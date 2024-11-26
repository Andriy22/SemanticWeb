import React, {useEffect, useState} from 'react';
import {Input} from "@/components/ui/input.tsx";
import {getScientists} from "@/services/api.ts";
import {Scientist} from "@/types/Scientist.ts";
import {Link} from "react-router-dom";

const Home = () => {
    const [name, setName] = useState<string>('')
    const [data, setData] = useState<Scientist[]>([])

    const handleChange = (e: React.FormEvent<HTMLInputElement>) => {
        setName(e.currentTarget.value)
    }

    useEffect(() => {
        getScientists(name).then((res) => {
            setData(res)
        });

    }, [name])

    return (
        <div>
            <div className={'flex justify-center'}>
                <Input type="text" onChange={handleChange} value={name} className={'md:w-1/2 w-9/12'}
                       placeholder="Query (first name, surname)"/>
            </div>

            <div className={'flex justify-center mt-10'}>
                <div className={'md:w-1/2 w-9/12'}>
                    <div className='grid gap-4 grid-cols-3'>
                        {data.map(item => (
                            <div
                                key={item.id}
                                className="border-b-gray-500 border-2">
                                <a href="#">
                                    <img className="rounded-t-lg h-80 w-full"
                                         onError={() => {
                                             setData(data.map((i) => {
                                                 if (i.uniqueWikiId === item.uniqueWikiId) {
                                                     return {
                                                         ...i,
                                                         imageUrl: 'https://icons.veryicon.com/png/o/internet--web/web-interface-flat/6606-male-user.png'
                                                     }
                                                 }
                                                 return i
                                             }))
                                         }}
                                         src={item?.imageUrl}
                                         alt=""/>
                                </a>
                                <div className="p-5">
                                    <Link to={`/${item.uniqueWikiId}`}>
                                        <h5 className="mb-2 text-2xl font-bold tracking-tight text-gray-900 dark:text-white">{item.fullname.split('@')[0]}</h5>
                                    </Link>
                                    <Link to={`/${item.uniqueWikiId}`} className="inline-flex items-center px-3 py-2 text-sm font-medium text-center
                                        text-white bg-pink-400 rounded-lg hover:bg-pink-800 focus:ring-4
                                        focus:outline-none focus:ring-pink-300 dark:bg-pink-600 dark:hover:bg-pink-700
                                        dark:focus:ring-pink-800"
                                    >
                                        Детальніше
                                    </Link>
                                </div>
                            </div>))}
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Home;