class PlantStorage
{
    public Dictionary<int, Plant> plants;
    public int idCounter = 1;

    public PlantStorage()
    {
        Plant plant1 = new(idCounter, "Casa Blanca Lily", 10, true, null); idCounter++;
        Plant plant2 = new(idCounter, "Elsa Sass Peony", 10, true, null); idCounter++;
        Plant plant3 = new(idCounter, "Sarah Bernhardt Peony", 15, true, null); idCounter++;
        Plant plant4 = new(idCounter, "Stargazer Lily", 7, true, null); idCounter++;
        Plant plant5 = new(idCounter, "Purple Bearded Iris", 7, true, null); idCounter++;
        Plant plant6 = new(idCounter, "Miniature Daffodil", 5, true, null); idCounter++;
        Plant plant7 = new(idCounter, "Daylily Yellow", 7, true, null); idCounter++;
        Plant plant8 = new(idCounter, "Daylily Pink", 7, true, null); idCounter++;
        Plant plant9 = new(idCounter, "Gardenia", 15, true, null); idCounter++;
        Plant plant10 = new(idCounter, "ZZ Plant", 15, true, null); idCounter++;
        Plant plant11 = new(idCounter, "Bamboo", 5, true, null); idCounter++;
        Plant plant12 = new(idCounter, "White Orchid", 10, true, null); idCounter++;
        Plant plant13 = new(idCounter, "Fiddle-Leaf Fig", 20, true, null); idCounter++;
    
        plants = []; 
        plants.Add(plant1.Id, plant1);    
        plants.Add(plant2.Id, plant2);    
        plants.Add(plant3.Id, plant3);    
        plants.Add(plant4.Id, plant4);    
        plants.Add(plant5.Id, plant5);    
        plants.Add(plant6.Id, plant6);    
        plants.Add(plant7.Id, plant7);    
        plants.Add(plant8.Id, plant8);    
        plants.Add(plant9.Id, plant9);    
        plants.Add(plant10.Id, plant10);    
        plants.Add(plant11.Id, plant11);    
        plants.Add(plant12.Id, plant12);    
        plants.Add(plant13.Id, plant13);    
    }

}