using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

using Android.Views;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using DotLiquid.Tags;
using Android.Support.V4.App;
using Android;
using Android.Content.PM;

namespace bando
{
    [Activity(Label = "@string/app_name", Theme = "@style/UberTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IOnMapReadyCallback
    {
       
        Android.Support.V7.Widget.Toolbar mainToolbar;
        Android.Support.V4.Widget.DrawerLayout drawerLayout;
        GoogleMap mainMap;
        readonly string[] permissionGroupLocation = { Manifest.Permission.AccessCoarseLocation, Manifest.Permission.AccessFineLocation };
        const int requestLocationId = 0;
        //public static MainActivity newInstance()
        //{
        //    MainActivity fragment = new MainActivity();
        //    return fragment;
        //}

        protected override void OnCreate(Bundle savedInstanceState)
        {
           base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);

            CheckLocationPermission();
            //  SupportMapFragment mapFragment = SupportFragmentManager.FindFragmentById(Resource.Id.map).JavaCast<SupportMapFragment>();
            // SupportMapFragment mapFragment = (SupportMapFragment)SupportFragmentManager.FindFragmentById(Resource.Id.map);

            //SupportMapFragment mapFragment = SupportFragmentManager.FindFragmentById(Resource.Id.map).JavaCast<SupportMapFragment>()
            // ;
            //SupportMapFragment.NewInstance(mainMap);
            //mapFragment.GetMapAsync(this);
            //     mapFragment.GetMapAsync(this);

            // SetUpMap();

            ConnectControl();
        }


        //private void SetUpMap()
        //{
        //    if(mainMap == null)
        //    {
        //        //FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map).GetMapAsync(this);
        //        FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map).GetMapAsync(this);
        //    }
        //}


        void ConnectControl()
        {
            drawerLayout = (Android.Support.V4.Widget.DrawerLayout)FindViewById(Resource.Id.drawerLayout);
            mainToolbar = (Android.Support.V7.Widget.Toolbar)FindViewById(Resource.Id.mainToolbar);
            SetSupportActionBar(mainToolbar);
            SupportActionBar.Title = "";
            Android.Support.V7.App.ActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Mipmap.ic_menu_action);
            actionBar.SetDisplayHomeAsUpEnabled(true);

        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer((int)GravityFlags.Left);
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);


            }
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            mainMap = googleMap;
        }

        //public void OnMapReady(GoogleMapOptions googleMapOptions)
        //{
        //    mainMap = googleMapOptions;



        //    // Add a marker in Sydney and move the camera
        //    //LatLng sydney = new LatLng(-34, 151);
        //    //mainMap.AddMarker(new MarkerOptions().SetPosition(sydney).SetTitle("Marker in Sydney"));
        //    //mainMap.MoveCamera(CameraUpdateFactory.NewLatLng(sydney));
        //}

        bool CheckLocationPermission()
        {
            bool permissionGranted = false;

            if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) != Android.Content.PM.Permission.Granted
                && ActivityCompat.CheckSelfPermission(this, Manifest.Permission.AccessCoarseLocation) != Android.Content.PM.Permission.Granted)
            {
                permissionGranted = false;
                RequestPermissions(permissionGroupLocation, requestLocationId);

            }
            else
                {
                permissionGranted = true;

            }
            return permissionGranted;


        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
           if(grantResults[0] == (int)Android.Content.PM.Permission.Granted)
            {
                Toast.MakeText(this, "Permission was granted", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(this, "Permission was dennied", ToastLength.Short).Show();
            }
        }


    }
}