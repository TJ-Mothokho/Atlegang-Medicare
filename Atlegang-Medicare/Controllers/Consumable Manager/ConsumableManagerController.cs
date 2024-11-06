using Microsoft.AspNetCore.Mvc;
using DataAccesslayer.Services;
using DataAccesslayer.Models.Domains.Consumable_and_Script_Manager;
using DataAccesslayer.Repository.Consumable_And_Script_Manager;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccesslayer.Repository.Administrator;
using DataAccesslayer.Models.View_Models.ConsumerManager;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Atlegang_Medicare.Controllers.Consumable_Manager
{
    [Authorize(1, 4)]
    public class ConsumableManagerController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConsumableRepository _consumableRepository;
        private readonly IWardRepository _wardRepository;
        private readonly IStockRepository _stockRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        public static List<OrderDetail> listOfOrders = new List<OrderDetail>();


        public ConsumableManagerController(IWardRepository wardRepository, IHttpContextAccessor contextAccessor, IConsumableRepository consumableRepository, IStockRepository stockRepository, ISupplierRepository supplierRepository, IOrderRepository orderRepository, IOrderDetailRepository _orderDetailRepository)
        {
            _contextAccessor = contextAccessor;
            _consumableRepository = consumableRepository;
            _wardRepository = wardRepository;
            _stockRepository = stockRepository;
            _supplierRepository = supplierRepository;
            _orderRepository = orderRepository;
            this._orderDetailRepository = _orderDetailRepository;
        }


        public async Task<IActionResult> Index()
        {
            // Get all necessary data
            var wards = await _wardRepository.GetAllWardsAsync();
            var stock = await _stockRepository.GetAllStocksAsync();
            var consumables = await _consumableRepository.GetAllConsumablesAsync();
            var dashboardNumbers = new List<object>();

            // done
            ViewBag.WardThresholds = await _consumableRepository.GetWardsBelowThreshold();
            ViewBag.Suppliers = await _consumableRepository.GetNumberOfSuppliers();
            ViewBag.NumOfConsumables = await _consumableRepository.GetNumberOfConsumables();
            ViewBag.OrdersPending = await _consumableRepository.GetOrdersPending();
                                 

            return View();
        }

        public async Task<IActionResult> Dashboard()
        {
            var wards = await _wardRepository.GetAllWardsAsync();
            var stock = await _stockRepository.GetAllStocksAsync();
            var consumables = await _consumableRepository.GetAllConsumablesAsync();

            var result = new List<object>();

            foreach (var consumable in consumables)
            {
                var filteredData = from item in stock
                                   join ward in wards on item.WardID equals ward.WardID
                                   where item.ConsumableID == consumable.ConsumableID
                                   select new
                                   {
                                       ward.Name,
                                       item.Quantity
                                   };

                result.Add(new
                {
                    labels = filteredData.Select(x => x.Name).ToArray(),
                    data = filteredData.Select(x => x.Quantity).ToArray(),
                    label = consumable.ConsumableName
                });
            }

            return Json(result);
        }

        public async Task<IActionResult> ViewConsumables()
        {
            //Get all consumables from the Database
            var consumables = await _consumableRepository.GetAllConsumablesAsync();

            if (ViewBag.ConsumablesTable == null)
            {
                //Save the consumables in a ViewBag to display the Table in the view
                ViewBag.ConsumablesTable = consumables;
            }


            //Save the consumables in a ViewBag to display in the dropdown. Show ConsumableName, Save ConsumableID
            ViewBag.ConsumableDropDown = new SelectList(consumables, "ConsumableID", "ConsumableName");


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ViewConsumables(int consumableID)
        {
            //The consumableID passed will be used to filter the table and show that specific consumable

            //Get all consumables from the Database
            var consumables = await _consumableRepository.GetAllConsumablesAsync();
            //Save the consumables in a ViewBag to display in the dropdown. Show ConsumableName, Save ConsumableID
            ViewBag.ConsumableDropDown = new SelectList(consumables, "ConsumableID", "ConsumableName");

            //If no consumable was selected (Happens when the user clicks "Reset" button), return to View
            if (consumableID == 0)
            {
                return RedirectToAction("ViewConsumables");
            }

            //Search that specific consumabe and store it in consuable variable
            var consumable = await _consumableRepository.FilterConsumableByIdAsync(consumableID);

            //Save the consumables in a ViewBag to display the Table in the view
            ViewBag.ConsumablesTable = consumable;

            return View();
        }




        //Add Starts Here
        public async Task<IActionResult> AddConsumable()
        {
            // Fetch all suppliers to populate the supplier dropdown in the view.
            var suppliers = await _supplierRepository.GetAllSuppliersAsync();

            // Populate the ViewBag with a list of suppliers, setting SupplierID as the value and Name as the text to display.
            ViewBag.SupplierList = new SelectList(suppliers, "SupplierID", "Name");



            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddConsumable(Consumable consumable)
        {
            try
            {
                // Attempt to add the consumable.
                var result = await _consumableRepository.AddConsumableAsync(consumable);

                if (result)
                {
                    // If the addition is successful, display a success message using TempData.
                    TempData["Success"] = "Consumable added successfully";
                }
                else
                {
                    // If the addition fails, display an error message.
                    TempData["Error"] = "Consumable not added, Try again.";
                }
            }
            catch (Exception ex)
            {
                // If an exception occurs, display an error message with the exception details.
                TempData["Error"] = $"Error: {ex} \n Contact Administrator immediately";
            }

            return View();
        }


        //Update Starts Here
        public async Task<IActionResult> UpdateConsumable(int consumableID)
        {
            // Fetch all suppliers to populate the supplier dropdown in the view.
            var suppliers = await _supplierRepository.GetAllSuppliersAsync();

            // Populate the ViewBag with a list of suppliers, setting SupplierID as the value and Name as the text to display.
            ViewBag.SupplierList = new SelectList(suppliers, "SupplierID", "Name");

            try
            {
                // Fetch the consumable by its ID.
                var consumable = await _consumableRepository.GetConsumableByIdAsync(consumableID);

                // Check if the consumable exists.
                if (consumable == null)
                {
                    // If the consumable is not found, display a message and redirect to the consumables list view.
                    TempData["Message"] = "Consumable not found";
                    return RedirectToAction("ViewConsumables");
                }

                // If the consumable is found, pass it to the view to display the update form.
                return View(consumable);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur, display an error message, and redirect to the consumables list view.
                TempData["Message"] = $"Error: {ex} \n Contact Administrator immediately";
                return RedirectToAction("ViewConsumables");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateConsumable(Consumable consumable)
        {
            try
            {
                // Try to update the consumable in the database.
                if (await _consumableRepository.UpdateConsumableAsync(consumable))
                {
                    // If the update is successful, display a success message and redirect to the consumables list view.
                    TempData["Success"] = "Consumable updated successfully";
                }
                else
                {
                    // If the update fails, display a failure message and redisplay the form.
                    TempData["Error"] = "Consumable not updated, Try again.";
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur, display an error message, and redisplay the form.
                TempData["Error"] = $"Error: {ex} \n Contact Administrator immediately";

            }

            return View();
        }
        //Update Ends Here


        //Delete Starts Here
        [HttpPost]
        public async Task<IActionResult> DeleteConsumable(int consumableID)
        {
            await _consumableRepository.DeleteConsumableAsync(consumableID);
            return RedirectToAction("ViewConsumables");
        }
        //Delete Ends Here



        //Stock Take Starts Here
        public async Task<IActionResult> StockTake()
        {
            //Get all consumables from the Database
            var consumables = await _consumableRepository.GetAllConsumablesAsync();
            var wards = await _wardRepository.GetAllWardsAsync();
            var stocks = await _stockRepository.GetAllStocksAsync();

            //join Stock, Consumables and Ward Tables
            var table = from stock in stocks
                        join ward in wards
                        on stock.WardID equals ward.WardID
                        join consumable in consumables
                        on stock.ConsumableID equals consumable.ConsumableID
                        select new
                        {
                            WardStockID = stock.WardStockID,
                            ward.WardID,
                            WardName = ward.Name, 
                            consumable.ConsumableID,
                            ConsumableName = consumable.ConsumableName,
                            Quantity = stock.Quantity
                        };

            //Save the consumables in a ViewBag to display the Table in the view
            ViewBag.StocksTable = table;
            


            //Save the consumables in a ViewBag to display in the dropdown. Show ConsumableName, Save ConsumableID
            ViewBag.ConsumableDropDown = new SelectList(consumables, "ConsumableID", "ConsumableName");
            ViewBag.WardsDropDown = new SelectList(wards, "WardID", "Name");


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> StockTake(Stock stock)
        {
            try
            {
                // Attempt to add the consumable.
                if (await _stockRepository.UpdateStockAsync(stock))
                {
                    // If the addition is successful, display a success message using TempData.
                    TempData["Success"] = "Stock updated successfully";
                }
                else
                {
                    // If the addition fails, display an error message.
                    TempData["Error"] = "Stock not updated, Try again.";
                }
            }
            catch (Exception ex)
            {
                // If an exception occurs, display an error message with the exception details.
                TempData["Error"] = $"Error: {ex} \n Contact Administrator immediately";
            }

            return RedirectToAction("StockTake");
        }
        //Stock Take Ends Here





        //Orders Start Here

        public async Task<IActionResult> Orders()
        {
            //Get Consumables from Database
            var consumables = await _consumableRepository.GetAllConsumablesAsync();
            //Save the consumables in a ViewBag to display in the dropdown. Show ConsumableName, Save ConsumableID
            ViewBag.ConsumableDropDown = new SelectList(consumables, "ConsumableID", "ConsumableName");

            //Save the orderdetails in a Viewbag to show in the view. Using LINQ to join the Consumables and OrderDetails to display the Consumable Name
            ViewBag.CartItems = from item in listOfOrders
                                join consumable in consumables on item.ConsumableID equals consumable.ConsumableID
                                select new
                                {
                                    ConsumableID = item.ConsumableID,
                                    Quantity = item.Quantity,
                                    Message = item.Message,
                                    ConsumableName = consumable.ConsumableName,
                                    Price = consumable.Cost,
                                    TotalCost = item.Quantity * consumable.Cost
                                };

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(OrderDetail item)
        {
            // Initialize listOfOrders if it's null
            if (listOfOrders == null)
            {
                listOfOrders = new List<OrderDetail>();
            }

            //Save the message as an empty string if it's null
            if (item.Message == null)
            {
                item.Message = "";
            }
            
            //Add an item to the list of pending orders (add to cart basically)
            listOfOrders.Add(item);

            return RedirectToAction("Orders");
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(int managerID)
        {
            //Get Consumables from Database
            var consumables = await _consumableRepository.GetAllConsumablesAsync();

            var prices = from item in listOfOrders
                         join consumable in consumables on item.ConsumableID equals consumable.ConsumableID
                         select new
                         {
                             TotalCost = item.Quantity * consumable.Cost
                         };

            double totalCost = 0;
            foreach (var price in prices)
            {
                totalCost = totalCost + price.TotalCost;
            }

            Order order = new Order
            {
                OrderDate = DateTime.Now,
                ManagerID = managerID,
                TotalCost = totalCost
            };

            //Save the order in the database
            var result = await _orderRepository.AddOrderAsync(order);
            try
            {
                if(result)
                {
                    var recentOrder = await _orderRepository.GetOrderAddedAsync(managerID);
                    if (recentOrder == null)
                    {
                        TempData["Error"] = "Failed to retrieve Script.";
                        return View();
                    }

                    foreach(var item in listOfOrders)
                    {
                        var orderDetail = new OrderDetail
                        {
                            ConsumableID = item.ConsumableID,
                            Quantity = item.Quantity,
                            OrderID = recentOrder.OrderID,
                            Message = item.Message
                        };

                        var isAdded = await _orderDetailRepository.AddOrderDetailAsync(orderDetail);
                    }

                    // If the addition is successful, display a success message using TempData.
                    TempData["Success"] = "Order was successful";
                }
                else
                {
                    // If the addition fails, display an error message.
                    TempData["Error"] = "Order not successful, Try again.";
                }
                listOfOrders.Clear();
            }
            catch (Exception ex)
            {
                // If an exception occurs, display an error message with the exception details.
                TempData["Error"] = $"Error: {ex} \n Contact Administrator immediately";
                listOfOrders.Clear();
            }

            //Retrive orderID from the database


            //Save the order details in the database

            return RedirectToAction("Orders");
        }

        public async Task<IActionResult> OrderHistory()
        {
            ViewBag.OrderHistory = await _orderRepository.GetAllOrdersAsync();

            return View();
        }

        public async Task<IActionResult> OrderDetails(int orderID)
        {
            var order = await _orderDetailRepository.GetOrderDetailsByOrderID(orderID);

            ViewBag.OrderDetails = order;
            return View();
        }

        //To clear my TempData everytime i run a new action
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            TempData.Clear();
        }
    }
}
