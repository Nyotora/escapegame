using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTest
{
    
    // A Test behaves as an ordinary method
    [Test]
    public void Open_door_when_interact_once_with_closed_door()
    {
        Player player = new Player();
        Door door = new Door(false);

        door.Interact(player);

        Assert.IsTrue(door.IsOpen());

    }

    // A Test behaves as an ordinary method
    [Test]
    public void Add_item_in_inventory_when_interact_with_StudentTable()
    {
        Player player = new Player();
        StudentTable studentTable = new StudentTable("ETUDIANT", "Fiche qui a une table sql");

        studentTable.Interact(player);

        Assert.IsTrue(player.GetInventory().Contains(studentTable));

    }

    [Test]
    public void Add_only_one_item_in_inventory_when_interact_twice_with_StudentTable()
    {
        Player player = new Player();
        StudentTable studentTable = new StudentTable("ETUDIANT", "Fiche qui a une table sql");

        studentTable.Interact(player);
        studentTable.Interact(player); 

        Assert.AreEqual(player.GetInventory().Count, 1);

    }

    /*
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayerTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }*/
}
