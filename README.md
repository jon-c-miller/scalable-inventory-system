### Scalable Inventory System

An inventory system that uses scriptable objects to enable simple creation and organization of an item database.

<details>
	<summary>Project Overview</summary>
	<ul><br>
		<b>· Core Direction ·</b>
		<br>
		The goal for this project was to create a simple and approachable inventory system using structs and a database built around scriptable objects, which greatly improves productivity and workflow in regards to prototyping and balancing of items. In this project, scriptable objects replace what would either be a class or text file-based item and stat database, allowing a developer to rapidly create and tweak a set of customized item and stat templates in the editor.
		<br><br>
		To ensure that this project would be actually useable in a game, moderate scope increases were allowed, and even encouraged, as long as they contributed to truly putting the API to the test and ensuring it was functional and reliable. This resulted in a development that was highly organic and iterative, and that more or less resolved its own issues by revealing unfavorable constraints and suggesting pragmatic design.
		<br><br>
		The result was a scene that provided a way to execute by input many of the most common commands related to managing and browsing an item inventory, with an arbitrary view implementation showing the results via a conventional text-based item list and a familiar RPG-style item tooltip.
		<br><br>
		<b>· Secondary Goals ·</b>
		<br>
		In tune with the general design strategy for past and future projects, a focus on designing for scalability and the practical needs to accomplish it was foremost during planning and iteration. The choice of using struct rather than class for item instances keeps the system more predictable, greatly reduces reference count, and avoids the infamous null exception issue. In addition, making use of interfaces in areas of the system certain to see revisions (namely item and inventory views) simultaneously helps enforce structure and keep the system adaptable as the project evolves.
		<br><br>
		Also, in keeping with best practices while using version control, another objective with this project was to ensure that each update was in a working state and the degree of each change was minor and truly incremental. There were several major changes in direction due to the iterative nature of the project; and at every stage, designing in a way that small steps could be taken to safely tackle large changes was prioritized.
	</ul>
</details>

<details>
	<summary>Elements</summary>
	<ul><br>
		<b>· Game API ·</b>
		<br>
		This element binds the project together, providing a single point of access to organize communication between classes/elements and execute high level commands. To ensure that access to the game API is goal-oriented, and to provide structure and predictability to the system, the API is defined via method calls spread out over multiple files of a partial class; and access to each individual manager in the system is prohibited.
		<br></br>
		<b>· Inventory Item ·</b>
		<br>
		The item portion of this element is comprised of two parts: InventoryItem, a struct representing a generated item in the world or in a player's inventory; and SInventoryItem, a scriptable object template for storing item identifying information and generation constraints. Item and stat templates can be easily created in the editor via dropdown menu, and their properties defined in the inspector.
		<br><br>
		In this implementation of items, there is a primary stat (such as armor or damage) that defines the item's role, and optional secondary stats. Limiting which stats can serve as secondary stats for each item is defined by the developer in a collection in the item's scriptable object template. Stat generation itself is kept fairly basic, calculated using the desired item quality and level, as well as generic modifier and variation fields defined in the stats themselves. The number of secondary stats added to an item is determined by the item's desired quality level. This system ensures a great deal of flexibility, as simply deciding beforehand on the game's max possible item level provides a foundation for general stat and item balancing. Items can also optionally be given an unlock level, which provides a simple level-gating solution that prevents all items from being available at the beginning of the game.
		<br><br>
		The stats (properties) portion of this element is comprised of two parts: InventoryItemStat, a struct representing a generated stat on an item; and SInventoryItemStat, a scriptable object template for storing the stat's generation constraints. Keeping the stats simple allows for a powerfully flexible system where they can be easily balanced against one another, and also adapt to different forms of item implementations. A flag for indicating whether the stat should be treated as a percentage is also present to permit percentage-based stats alongside direct value changing stats.
		<br><br>
		There is an additional view aspect to this element, a visual representation of an item in the vague fashion of an info tooltip or entry in an inventory navigation menu. This allows for effective testing and demonstrates practical utilization of the item API. The view class is hidden behind an interface, IItemView, which allows for various view implementations to be developed and swapped out easily.
		<br><br>
		<b>· Inventory Manager ·</b>
		<br>
		This element represents a runtime instance of a unit's inventory, and is comprised of several parts, including algorithms to manipulate the inventory, as well as a database of the scriptable object item and stat templates. Due to the complexity and length of the algorithms, each individual operation (add, remove, compact, generate, etc.) was split off into a separate class, and some of them are described here.
		<br><br>
		The adding algorithm is the most complex due to item stackability. For stackable items, it loops through the inventory from the beginning, searching for an entry with an identical type. If found, it can either increase that entry's stack amount by the added item's stack amount, or max the entry's stack amount and add a new stack. Whether surplus from the stackable item being added after filling the current stack results in a new stack entry depends on the inventory's settings for allowing multiple stacks. For non-stackable items, it loops through the inventory from the beginning and assigns the item being added to the first index with an empty entry. A check to ensure that the inventory limit has not been reached is always carried out before approving the addition.
		<br><br>
		The removal by type algorithm is less involved, and essentially seeks to remove the entry of the matching item type that is highest in the inventory collection. This supports keeping a condensed inventory, whereas otherwise it would theoretically become more and more fragmented and items spread thin across the limit of the inventory if distant entries are ignored as items are added and removed. It loops through the inventory from the end, searching for an entry with an identical type. If found, it checks to see if the item being removed is flagged as stackable and reduces the stack count or removes it accordingly.
		<br><br>
		The item generation algorithm was designed to maximize use of the system created for items and stats, and it retrieves the appropriate item and stat templates during the generation process by accessing the inventory database through GameAPI. The number of secondary stats generated for an item is determined by casting the desired item quality value to integer, an arrangement that lets the mapped integers in the item quality enum represent the amount of stats desired at each quality level; which, in this implementation, gives every quality level over the base level an additional stat.
		<br><br>
		There is an additional view aspect to this element, a visual representation of a list-type inventory which displays the names of item entries and includes their quantities. A set of six unmoving stacked elements, each representing an item entry, displays a subset, or 'window', of the area of the inventory being viewed. Scrolling up and down simply updates the information on each element, giving the impression of navigation. Simple features, such as skipping empty spaces and defining the amount of concurrently displayed items in the list are available; as well as are options to enable multiple stacks and specify stack sizes. The view class is hidden behind an interface, IInventoryView, which allows for various view implementations to be developed and swapped out easily.
		<br><br>
		<b>· InventoryTester ·</b>
		<br>
		This element serves via user input as a proxy to demonstrate interaction with the inventory manager via GameAPI. It provides the ability to test generation of individual items, navigate the inventory and view item details, add and remove items, compact the inventory, and add a specified amount of randomly generated items to the inventory. There is also a Current Unlock Level field, which acts to demonstrate the basic level-gating system, and affects which items are generated (items with unlock levels above the Current Unlock Level will not be generated).  
		<br><br>
	</ul>
</details>

<details>
	<summary>Reflection On Development</summary>
	<ul><br>
		<b>· State, Templates, and Hosting ·</b>
		<br>
		In the early stages of design, state fields were inadvertently added to the item and item stat scriptable objects. While keeping state in a scriptable object may be fine for settings, it would quickly become unmanageable for an inventory system meant to be scalable: there would have to be a scriptable object file for every single item in the inventory. This oversight was corrected over several design steps, eventually ensuring that all state was strictly kept in the element meant to represent actual instances of an item.
		<br><br>
		Another design point that was significantly changed was how the InventoryItem and InventoryItemStat elements were initially meant to host an actual scriptable object, keeping a reference to it. While this is an approach that may be useful in some situations, it again ran counter to the goal of scalability. For example, having 200 inventory item instances with scriptable object references, plus those of all their stats would arguably be a nightmare scenario.
		<br><br>
		To prevent this type of situation, the template concept was again utilized and the design kept as simple as possible: InventoryItem and InventoryItemStat were made into structs to keep the minimum amount of state for a given item instance during runtime. Then, their companion scriptable objects were redesigned to store name and description values, as well as house options for use during item generation.
		<br><br>
		<b>· Planning and Replanning ·</b>
		<br>
		In hindsight, the lack of key usage of scriptable objects in previous projects coupled with an overly brief initial planning stage contributed greatly to some early design hiccups, especially with regards to fields on the item and stat templates as well as the type used for item instances. However, taking a short break from the code and editor and setting aside some time with pen and paper quickly helped solidify the overall vision for this project, paved the way for some healthy scope increases, and eliminated in advance many potential issues that would have cropped up in later development.
		<br><br>
		Particularly during later development of the inventory system when designing for efficient adding, removing, and compacting algorithms, laying out the loop structure first in pseudocode with pen and paper helped immensely to keep productivity high and smooth, and more or less resulted in the system working as intended with only a few tweaks. Even if previous projects had not led well to a realization of just how effective a tool writing in pen before writing in code is, this project would have invariably driven the point home.
		<br><br>
		One notable benefit during an iteration phase focusing on simplification was having the potential to eliminate the name and description fields from item instances themselves, allowing the item type to serve as a proxy. Simply accessing the item database's collection of scriptable objects via the item type allowed for any part of the codebase needing this information to obtain it easily. This design compensates for and avoids a theoretical situation where there are thousands of items that would otherwise need to superfluously keep fields with name and description in memory.
		<br><br>
		<b>· Unstacking Stackability Challenges ·</b>
		<br>
		Development of the inventory system and the perspective it offered due to the item API's freedoms and limitations afforded multiple opportunities to iterate on the item API and make practical simplifications.
		<br><br>
		Most notably, parsing the inventory when adding an item was compounded in complexity not only by the fact that items can be stackable (thus precluding simply adding a new entry), but also by the fact that stackability was initially queried by accessing the generated item's stats. This meant that in order to determine if an item was stackable, the item's ItemStats collection had to be iterated through; which is a loop through the entire inventory, and a nested loop at each iteration to search the item's stats collection for stackability.
		<br><br>
		It was quickly realized that the algorithm was unnecessarily complicated simply due to the nature of how stackability was tracked and queried, and that a change was needed. Having stackability represented as a stat type in the generated item also necessitated other workarounds, and it was ultimately decided that stackability needed to be represented more approachably. To this end, a simple boolean in the generated item was used instead of keeping the stackability as an entry in its stat collection. In order to enable multiple copies of a stackable item in one drop, a maximum drop count field was also added for use during item generation.
		<br><br>
		<b>· Adapting for Scalability ·</b>
		<br>
		At a certain point during development, the implementation of the inventory's API was as an extension of a central game API that enforced strictly controlled access to game systems by simply having each possible way the game's API could be accessed defined in a method call. As an arbitrary example, this meant that in practice, any part of the codebase could call a method available in the game's API like CompactInventory(). While that offers healthy freedom within defined limits, it also means that a single file holds every method representing the game's API, which could be very unpleasant to navigate through in a larger game.
		<br><br>
		It became clear that while the structure of the initial game coordinator implementation may be well-suited to smaller projects, a slight change of architecture was needed to truly provide greater scalability and organization of the game's API. To this end, it was decided that the central Instance singleton filtering API access to the codebase would refer not to the Game instance itself, but rather a partial plain class instantiated by the Game instance.
		<br><br>
		This allowed for each part of the game to have the methods for its API contained within its own file, accomplishing the ideal objective of having small and contained portions of related code grouped together, as well as maintaining the desire for a single point of entry into the game's API via the Instance reference. This had the predictable effect of transitioning the game class containing the Instance singleton to, regardless of scale, be clean and permanently freed of manager-type references and possibly thousands of API access methods.
		<br><br>
		On another front, a cleaner and more approachable experience when editing the game's managers in the inspector was effected by rearranging some of the class and object hierarchies, making use of GameObject references to obtain interfaces where indirection was desired, aiming to keep view as separate and interchangeable, and generally ensuring that functionality could be self-contained in a single game object to support a healthy prefab infrastructure.
		<br><br>
		<b>· Deciding On Item Style ·</b>
		<br>
		The initial development for item style, including how stats would work and how they would be generated, used Diablo-type items as a conceptual foundation. While this helped guide initial planning, hammering out the specific logistics of keeping the system simple yet flexible, and generic enough to work with multiple visions for the system required a highly iterative and almost experimental approach.
		<br><br>
		Item templates were initially given a single configurable collection of possible stats, the quantity of which would be assigned based on item quality. While this was sort of on the right track, it caused a few issues. For example, the presence (or absence) of stats would be completely based on quality; and it forced behavior-defining traits (like effect range or stackable) to be lumped in with randomly generated items. Adding a primary stat to each item, allowing items to always have a 'main' stat and assignable secondary stats based on quality, solved the first issue; and adding defining stats as a dedicated collection that defines how the item behaves or is handled solved the second. 
		<br><br>
		The item stat generation was also somewhat convoluted in early stages, forced too many constraints on the system, was unnecessarily complicated, and was generally very limiting due to being specific and specialized. Moving away from low/high fields and other ways to enforce value limits at the stat level created a lot of freedom to let the item design itself drive how the values in stats were calculated and used. To accomplish this, stats were rendered down to two main values: Modifier and Variance.
		<br><br>
		The modifier would serve as a way to balance one stat from another against the item itself, allowing the item to use its own level and quality to calculate appropriately and consistently weighted values for all of its stats. And the variance would work as a direct or percentage based value modifier during the stat's value calculation; how the variance is used would be entirely dependent on the developer. Keeping these two fields generic and adaptable theoretically enables a surprisingly wide variety of item system implementations; and the very nature of its simplicity offers a great launchpoint into stat balancing, which is an intricate affair entirely in and of itself.
	</ul>
</details>

<details>
	<summary>Ideas for Future Additions</summary>
	<ul><br>
		· Add support for more than one inventory, such as in a multiplayer game:
		<ul>
			- Either create a separate class to handle a unit's current inventory or leave it represented as a list
			<br>
			- Move the current inventory collection out of the inventory manager
			<br>
			- Add an inventories dictionary to the inventory manager with PlayerID as key and class or list as value
			<br>
			- Add PlayerID arguments/parameters to all relevant parts of the API
		</ul>
		<br>
		· Add a different item implementation to test and make use of the flexibility of the item stat design
		<br><br>
		· Add icons or images to items
		<br><br>
		· Add small item type indicators to the inventory viewer
	</ul>
</details>

<details open>
	<summary>How to Use the Project</summary>
	<ul><br>
		<b>1.</b> Open project in Unity3D as usual
		<br>
		<b>2.</b> Open Main scene
		<br>
		<b>3.</b> Adjust/Confirm keybindings and test parameters in InventoryTester
		<br>
		<b>4.</b> Adjust/Confirm inventory options in Game > Elements > Inventory Manager
		<br>
		<b>5.</b> Adjust/Confirm view options under Game/Inventory/InventoryView and /ItemView
		<br>
		<b>6.</b> Create additional items/stats if desired, and add them to Inventory Manager > Database
		<br>
		<b>7.</b> Play scene
		<br><br>
		<i>Created using Unity version 2022.3.9f1</i>
    </ul>
</details>