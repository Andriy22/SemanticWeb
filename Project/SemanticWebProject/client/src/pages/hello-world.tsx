import { useState, useEffect } from 'react';
import { Button } from '../components/ui/button';
import { Input } from '../components/ui/input';

export default function HelloWorld() {
  const [greeting, setGreeting] = useState<string>('');
  const [name, setName] = useState<string>('');
  const [loading, setLoading] = useState<boolean>(false);

  const fetchGreeting = async (nameParam?: string) => {
    setLoading(true);
    try {
      const query = nameParam ? `?name=${encodeURIComponent(nameParam)}` : '';
      const response = await fetch(`/api/helloworld/greeting${query}`);
      const data = await response.json();
      setGreeting(data.message);
    } catch (error) {
      console.error('Error fetching greeting:', error);
      setGreeting('Error loading greeting');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchGreeting();
  }, []);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    fetchGreeting(name);
  };

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-3xl font-bold mb-6">Hello World Page</h1>
      
      <div className="bg-white rounded-lg shadow p-6 mb-6">
        <h2 className="text-xl font-semibold mb-4">Greeting from API</h2>
        {loading ? (
          <p className="text-gray-500">Loading...</p>
        ) : (
          <p className="text-lg">{greeting}</p>
        )}
      </div>

      <div className="bg-white rounded-lg shadow p-6">
        <h2 className="text-xl font-semibold mb-4">Personalized Greeting</h2>
        <form onSubmit={handleSubmit} className="flex gap-2">
          <Input
            type="text"
            placeholder="Enter your name"
            value={name}
            onChange={(e) => setName(e.target.value)}
            className="flex-1"
          />
          <Button type="submit">Get Greeting</Button>
        </form>
      </div>
    </div>
  );
}