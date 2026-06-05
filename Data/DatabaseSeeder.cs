using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusTicketing.Models;
using Microsoft.EntityFrameworkCore;

namespace BusTicketing.Data
{
    public class DatabaseSeeder
    {
        private readonly ApplicationDbContext _db;

        public DatabaseSeeder(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task SeedAsync()
        {
            var municipalities = new List<Municipality>
            {
                CreateMunicipality("Alburquerque", new[] { "Bahi", "Basacdacu", "Cantiguib", "Dangay", "East Poblacion", "Ponong", "San Agustin", "Santa Filomena", "Tagbuane", "Toril", "West Poblacion" }),
                CreateMunicipality("Alicia", new[] { "Cabatang", "Cagongcagong", "Cambaol", "Cayacay", "Del Monte", "Katipunan", "La Hacienda", "Mahayag", "Napo", "Pagahat", "Poblacion", "Progreso", "Putlongcam", "Sudlon", "Untaga" }),
                CreateMunicipality("Anda", new[] { "Almaria", "Bacong", "Badiang", "Buenasuerte", "Candabong", "Casica", "Katipunan", "Linawan", "Lundag", "Poblacion", "Santa Cruz", "Suba", "Talisay", "Tanod", "Tawid", "Virgen" }),
                CreateMunicipality("Antequera", new[] { "Angilan", "Bantolinao", "Bicahan", "Bitaugan", "Bungahan", "Can-omay", "Canlaas", "Cansibuan", "Celing", "Danao", "Danicop", "Mag-aso", "Poblacion", "Quinapon-an", "Santo Rosario", "Tabuan", "Tagubaas", "Tupas", "Ubojan", "Viga", "Villa Aurora" }),
                CreateMunicipality("Baclayon", new[] { "Buenaventura", "Cambanac", "Dasitam", "Guiwanon", "Landican", "Laya", "Libertad", "Montana", "Pamilacan", "Payahan", "Poblacion", "San Isidro", "San Roque", "San Vicente", "Santa Cruz", "Taguihon", "Tanday" }),
                CreateMunicipality("Balilihan", new[] { "Baucan Norte", "Baucan Sur", "Boctol", "Boyog Norte", "Boyog Proper", "Boyog Sur", "Cabad", "Candasig", "Cantalid", "Cantomimbo", "Cogon", "Datag Norte", "Datag Sur", "Del Carmen Este", "Del Carmen Norte", "Del Carmen Sur", "Del Carmen Weste", "Del Rosario", "Dorol", "Haguilanan Grande", "Hanopol Este", "Hanopol Norte", "Hanopol Weste", "Magsija", "Maslog", "Sagasa", "Sal-ing", "San Isidro", "San Roque", "Santo Niño", "Tagustusan" }),
                CreateMunicipality("Batuan", new[] { "Aloja", "Behind The Clouds", "Cabacnitan", "Cambacay", "Cantigdas", "Garcia", "Janlud", "Poblacion Norte", "Poblacion Sur", "Poblacion Vieja", "Quezon", "Quirino", "Rizal", "Rosariohan", "Santa Cruz" }),
                CreateMunicipality("Bien Unido", new[] { "Bilangbilangan Dako", "Bilangbilangan Diot", "Hingotanan East", "Hingotanan West", "Liberty", "Malingin", "Mandawa", "Maomawan", "Nueva Esperanza", "Nueva Estrella", "Pinamgo", "Poblacion", "Puerto San Pedro", "Sagasa", "Tuboran" }),
                CreateMunicipality("Bilar", new[] { "Bonifacio", "Bugang Norte", "Bugang Sur", "Cabacnitan", "Cambigsi", "Campagao", "Cansumbol", "Dagohoy", "Owac", "Poblacion", "Quezon", "Riverside", "Rizal", "Roxas", "Subayon", "Villa Aurora", "Villa Suerte", "Yanaya", "Zamora" }),
                CreateMunicipality("Buenavista", new[] { "Anonang", "Asinan", "Bago", "Baluarte", "Bantuan", "Bato", "Bonotbonot", "Bugaong", "Cambuhat", "Cambus-oc", "Cangawa", "Cantomugcad", "Cantores", "Cantuba", "Catigbian", "Cawag", "Cruz", "Dait", "Eastern Cabul-an", "Hunan", "Lapacan Norte", "Lapacan Sur", "Lubang", "Lusong", "Magkaya", "Merryland", "Nueva Granada", "Nueva Montana", "Overland", "Panghagban", "Poblacion", "Puting Bato", "Rufo Hill", "Sweetland", "Western Cabul-an" }),
                CreateMunicipality("Calape", new[] { "Abucayan Norte", "Abucayan Sur", "Banlasan", "Bentig", "Binogawan", "Bonbon", "Cabayugan", "Cabudburan", "Calunasan", "Camias", "Canguha", "Catmonan", "Desamparados", "Kahayag", "Kinabag-an", "Labuon", "Lawis", "Liboron", "Lo-oc", "Lomboy", "Lucob", "Madangog", "Magtongtong", "Mandaug", "Mantatao", "Sampoangon", "San Isidro", "Santa Cruz", "Sojoton", "Talisay", "Tinibgan", "Tultugan", "Ulbujan" }),
                CreateMunicipality("Candijay", new[] { "Abihilan", "Anoling", "Boyo-an", "Cadapdapan", "Cambane", "Can-olin", "Canawa", "Cogtong", "La Union", "Luan", "Lungsoda-an", "Mahangin", "Pagahat", "Panadtaran", "Panas", "Poblacion", "San Isidro", "Tambongan", "Tawid", "Tubod", "Tugas" }),
                CreateMunicipality("Carmen", new[] { "Alegria", "Bicao", "Buenavista", "Buenos Aires", "Calatrava", "El Progreso", "El Salvador", "Guadalupe", "Katipunan", "La Libertad", "La Paz", "La Salvacion", "La Victoria", "Matin-ao", "Montehermoso", "Montesuerte", "Montesunting", "Montevideo", "Nueva Fuerza", "Nueva Vida Este", "Nueva Vida Norte", "Nueva Vida Sur", "Poblacion Norte", "Poblacion Sur", "Tambo-an", "Vallehermoso", "Villaflor", "Villafuerte", "Villarcayo" }),
                CreateMunicipality("Catigbian", new[] { "Alegria", "Ambuan", "Baang", "Bagtic", "Bongbong", "Cambailan", "Candumayao", "Causwagan Norte", "Hagbuaya", "Haguilanan", "Kang-iras", "Libertad Sur", "Liboron", "Mahayag Norte", "Mahayag Sur", "Maitum", "Mantasida", "Poblacion", "Poblacion Weste", "Rizal", "Sinakayanan", "Triple Union" }),
                CreateMunicipality("Clarin", new[] { "Bacani", "Bogtongbod", "Bonbon", "Bontud", "Buacao", "Buangan", "Cabog", "Caboy", "Caluwasan", "Candajec", "Cantoyoc", "Comaang", "Danahao", "Katipunan", "Lajog", "Mataub", "Nahawan", "Poblacion Centro", "Poblacion Norte", "Poblacion Sur", "Tangaran", "Tontunan", "Tubod", "Villaflor" }),
                CreateMunicipality("Corella", new[] { "Anislag", "Canangca-an", "Canapnapan", "Cancatac", "Pandol", "Poblacion", "Sambog", "Tanday" }),
                CreateMunicipality("Cortes", new[] { "De la Paz", "Fatima", "Loreto", "Lourdes", "Malayo Norte", "Malayo Sur", "Monserrat", "New Lourdes", "Patrocinio", "Poblacion", "Rosario", "Salvador", "San Roque", "Upper de la Paz" }),
                CreateMunicipality("Dagohoy", new[] { "Babag", "Cagawasan", "Cagawitan", "Caluasan", "Can-oling", "Candelaria", "Estaca", "La Esperanza", "Mahayag", "Malitbog", "Poblacion", "San Miguel", "San Vicente", "Santa Cruz", "Villa Aurora" }),
                CreateMunicipality("Danao", new[] { "Cabatuan", "Cantubod", "Carbon", "Concepcion", "Dagohoy", "Hibale", "Magtangtang", "Nahud", "Poblacion", "Remedios", "San Carlos", "San Miguel", "Santa Fe", "Santo Niño", "Tabok", "Taming", "Villa Anunciado" }),
                CreateMunicipality("Dauis", new[] { "Biking", "Bingag", "Catarman", "Dao", "Mariveles", "Mayacabac", "Poblacion", "San Isidro", "Songculan", "Tabalong", "Tinago", "Totolan" }),
                CreateMunicipality("Dimiao", new[] { "Abihid", "Alemania", "Baguhan", "Bakilid", "Balbalan", "Banban", "Bauhugan", "Bilisan", "Cabagakian", "Cabanbanan", "Cadap-agan", "Cambacol", "Cambayaon", "Canhayupon", "Canlambong", "Casingan", "Catugasan", "Datag", "Guindaguitan", "Guingoyuran", "Ile", "Lapsaon", "Limokon Ilaod", "Limokon Ilaya", "Luyo", "Malijao", "Oac", "Pagsa", "Pangihawan", "Puangyuta", "Sawang", "Tangohay", "Taongon Cabatuan", "Taongon Can-andam", "Tawid Bitaog" }),
                CreateMunicipality("Duero", new[] { "Alejawan", "Angilan", "Anibongan", "Bangwalog", "Cansuhay", "Danao", "Duay", "Guinsularan", "Imelda", "Itum", "Langkis", "Lobogon", "Madua Norte", "Madua Sur", "Mambool", "Mawi", "Payao", "San Antonio", "San Isidro", "San Pedro", "Taytay" }),
                CreateMunicipality("Garcia Hernandez", new[] { "Abijilan", "Antipolo", "Basiao", "Cagwang", "Calma", "Cambuyo", "Canayaon East", "Canayaon West", "Candanas", "Candulao", "Catmon", "Cayam", "Cupa", "Datag", "Estaca", "Libertad", "Lungsodaan East", "Lungsodaan West", "Malinao", "Manaba", "Pasong", "Poblacion East", "Poblacion West", "Sacaon", "Sampong", "Tabuan", "Togbongon", "Ulbujan East", "Ulbujan West", "Victoria" }),
                CreateMunicipality("Getafe", new[] { "Alumar", "Banacon", "Buyog", "Cabasakan", "Campao Occidental", "Campao Oriental", "Cangmundo", "Carlos P. Garcia", "Corte Baud", "Handumon", "Jagoliao", "Jandayan Norte", "Jandayan Sur", "Mahanay", "Nasingin", "Pandanon", "Poblacion", "Saguise", "Salog", "San Jose", "Santo Niño", "Taytay", "Tugas", "Tulang" }),
                CreateMunicipality("Guindulman", new[] { "Basdio", "Bato", "Bayong", "Biabas", "Bulawan", "Cabantian", "Canhaway", "Cansiwang", "Casbu", "Catungawan Norte", "Catungawan Sur", "Guinacot", "Guio-ang", "Lombog", "Mayuga", "Sawang", "Tabajan", "Tabunok", "Trinidad" }),
                CreateMunicipality("Inabanga", new[] { "Anonang", "Badiang", "Baguhan", "Bahan", "Banahao", "Baogo", "Bugang", "Cagawasan", "Cagayan", "Cambitoon", "Canlinte", "Cawayan", "Cogon", "Cuaming", "Dagnawan", "Dagohoy", "Dait Sur", "Datag", "Fatima", "Hambongan", "Ilaud", "Ilaya", "Ilihan", "Lapacan Norte", "Lapacan Sur", "Lawis", "Liloan Norte", "Liloan Sur", "Lomboy", "Lonoy Cainsican", "Lonoy Roma", "Lutao", "Luyo", "Mabuhay", "Maria Rosario", "Nabuad", "Napo", "Ondol", "Poblacion", "Riverside", "Saa", "San Isidro", "San Jose", "Santo Niño", "Santo Rosario", "Sua", "Tambook", "Tungod", "U-og", "Ubujan" }),
                CreateMunicipality("Jagna", new[] { "Alejawan", "Balili", "Boctol", "Bunga Ilaya", "Bunga Mar", "Buyog", "Cabunga-an", "Calabacita", "Cambugason", "Can-ipol", "Can-uba", "Can-upao", "Canjulao", "Cantagay", "Cantuyoc", "Faraon", "Ipil", "Kinagbaan", "Laca", "Larapan", "Lonoy", "Looc", "Malbog", "Mayana", "Naatang", "Nausok", "Odiong", "Pagina", "Pangdan", "Poblacion", "Tejero", "Tubod Mar", "Tubod Monte" }),
                CreateMunicipality("Lila", new[] { "Banban", "Bonkokan Ilaya", "Bonkokan Ubos", "Calvario", "Candulang", "Catugasan", "Cayupo", "Cogon", "Jambawan", "La Fortuna", "Lomanoy", "Macalingan", "Malinao East", "Malinao West", "Nagsulay", "Poblacion", "Taug", "Tiguis" }),
                CreateMunicipality("Loay", new[] { "Agape", "Alegria Norte", "Alegria Sur", "Bonbon", "Botoc Occidental", "Botoc Oriental", "Calvario", "Concepcion", "Hinawanan", "Las Salinas Norte", "Las Salinas Sur", "Palo", "Poblacion Ibabao", "Poblacion Ubos", "Sagnap", "Tambangan", "Tangcasan Norte", "Tangcasan Sur", "Tayong Occidental", "Tayong Oriental", "Tocdog Dacu", "Tocdog Ilaya", "Villalimpia", "Yanangan" }),
                CreateMunicipality("Loboc", new[] { "Agape", "Alegria", "Bagumbayan", "Bahian", "Bonbon Lower", "Bonbon Upper", "Buenavista", "Bugho", "Cabadiangan", "Calunasan Norte", "Calunasan Sur", "Camayaan", "Cambance", "Candabong", "Candasag", "Canlasid", "Gon-ob", "Gotozon", "Jimilian", "Oy", "Poblacion Ondol", "Poblacion Sawang", "Quinoguitan", "Taytay", "Tigbao", "Ugpong", "Valladolid", "Villaflor" }),
                CreateMunicipality("Loon", new[] { "Agsoso", "Badbad Occidental", "Badbad Oriental", "Bagacay Katipunan", "Bagacay Kawayan", "Bagacay Saong", "Bahi", "Basac", "Basdacu", "Basdio", "Biasong", "Bongco", "Bugho", "Cabacongan", "Cabadug", "Cabug", "Calayugan Norte", "Calayugan Sur", "Cambaquiz", "Campatud", "Candaigan", "Canhangdon Occidental", "Canhangdon Oriental", "Canigaan", "Canmaag", "Canmanoc", "Cansuagwit", "Cansubayon", "Cantam-is Bago", "Cantam-is Baslay", "Cantaongon", "Cantumocad", "Catagbacan Handig", "Catagbacan Norte", "Catagbacan Sur", "Cogon Norte", "Cogon Sur", "Cuasi", "Genomoan", "Lintuan", "Looc", "Mocpoc Norte", "Mocpoc Sur", "Moto Norte", "Moto Sur", "Nagtuang", "Napo", "Nueva Vida", "Panangquilon", "Pantudlan", "Pig-ot", "Pondol", "Quinobcoban", "Sondol", "Song-on", "Talisay", "Tan-awan", "Tangnan", "Taytay", "Ticugan", "Tiwi", "Tontonan", "Tubodacu", "Tubodio", "Tubuan", "Ubayon", "Ubojan" }),
                CreateMunicipality("Mabini", new[] { "Abaca", "Abad Santos", "Aguipo", "Baybayon", "Bulawan", "Cabidian", "Cawayanan", "Concepcion", "Del Mar", "Lungsoda-an", "Marcelo", "Minol", "Paraiso", "Poblacion I", "Poblacion II", "San Isidro", "San Jose", "San Rafael", "San Roque", "Tambo", "Tangkigan", "Valaga" }),
                CreateMunicipality("Maribojoc", new[] { "Agahay", "Aliguay", "Anislag", "Bayacabac", "Bood", "Busao", "Cabawan", "Candavid", "Dipatlong", "Guiwanon", "Jandig", "Lagtangon", "Lincod", "Pagnitoan", "Poblacion", "Punsod", "Punta Cruz", "San Isidro", "San Roque", "San Vicente", "Tinibgan", "Toril" }),
                CreateMunicipality("Panglao", new[] { "Bil-isan", "Bolod", "Danao", "Doljo", "Libaong", "Looc", "Lourdes", "Poblacion", "Tangnan", "Tawala" }),
                CreateMunicipality("Pilar", new[] { "Aurora", "Bagacay", "Bagumbayan", "Bayong", "Buenasuerte", "Cagawasan", "Cansungay", "Catagda-an", "Del Pilar", "Estaca", "Ilaud", "Inaghuban", "La Suerte", "Lumbay", "Lundag", "Pamacsalan", "Poblacion", "Rizal", "San Carlos", "San Isidro", "San Vicente" }),
                CreateMunicipality("President Carlos P. Garcia", new[] { "Aguining", "Basiao", "Baud", "Bayog", "Bogo", "Bonbonon", "Butan", "Campamanog", "Canmangao", "Gaus", "Kabangkalan", "Lapinig", "Lipata", "Poblacion", "Popoo", "Saguise", "San Jose", "San Vicente", "Santo Rosario", "Tilmobo", "Tugas", "Tugnao", "Villa Milagrosa" }),
                CreateMunicipality("Sagbayan", new[] { "Calangahan", "Canmano", "Canmaya Centro", "Canmaya Diot", "Dagnawan", "Kabasacan", "Kagawasan", "Katipunan", "Langtad", "Libertad Norte", "Libertad Sur", "Mantalongon", "Poblacion", "Sagbayan Sur", "San Agustin", "San Antonio", "San Isidro", "San Ramon", "San Roque", "San Vicente Norte", "San Vicente Sur", "Santa Catalina", "Santa Cruz", "Ubojan" }),
                CreateMunicipality("San Isidro", new[] { "Abehilan", "Baryong Daan", "Baunos", "Cabanugan", "Caimbang", "Cambansag", "Candungao", "Cansague Norte", "Cansague Sur", "Causwagan Sur", "Masonoy", "Poblacion" }),
                CreateMunicipality("San Miguel", new[] { "Bayongan", "Bugang", "Cabangahan", "Caluasan", "Camanaga", "Cambangay Norte", "Capayas", "Corazon", "Garcia", "Hagbuyo", "Kagawasan", "Mahayag", "Poblacion", "San Isidro", "San Jose", "San Vicente", "Santo Niño", "Tomoc" }),
                CreateMunicipality("Sevilla", new[] { "Bayawahan", "Cabancalan", "Calinga-an", "Calinginan Norte", "Calinginan Sur", "Cambagui", "Ewon", "Guinob-an", "Lagtangan", "Licolico", "Lobgob", "Magsaysay", "Poblacion" }),
                CreateMunicipality("Sierra Bullones", new[] { "Abachanan", "Anibongan", "Bugsoc", "Cahayag", "Canlangit", "Canta-ub", "Casilay", "Danicop", "Dusita", "La Union", "Lataban", "Magsaysay", "Man-od", "Matin-ao", "Poblacion", "Salvador", "San Agustin", "San Isidro", "San Jose", "San Juan", "Santa Cruz", "Villa Garcia" }),
                CreateMunicipality("Sikatuna", new[] { "Abucay Norte", "Abucay Sur", "Badiang", "Bahaybahay", "Cambuac Norte", "Cambuac Sur", "Canagong", "Libjo", "Poblacion I", "Poblacion II" }),
                CreateMunicipality("Tagbilaran City", new[] { "Bool", "Booy", "Cabawan", "Cogon", "Dampas", "Dao", "Manga", "Mansasa", "Poblacion I", "Poblacion II", "Poblacion III", "San Isidro", "Taloto", "Tiptip", "Ubujan" }),
                CreateMunicipality("Talibon", new[] { "Bagacay", "Balintawak", "Burgos", "Busalian", "Calituban", "Cataban", "Guindacpan", "Magsaysay", "Mahanay", "Nocnocan", "Poblacion", "Rizal", "Sag", "San Agustin", "San Carlos", "San Francisco", "San Isidro", "San Jose", "San Pedro", "San Roque", "Santo Niño", "Sikatuna", "Suba", "Tanghaligue", "Zamora" }),
                CreateMunicipality("Trinidad", new[] { "Banlasan", "Bongbong", "Catoogan", "Guinobatan", "Hinlayagan Ilaud", "Hinlayagan Ilaya", "Kauswagan", "Kinan-oan", "La Union", "La Victoria", "Mabuhay Cabigohan", "Mahagbu", "Manuel M. Roxas", "Poblacion", "San Isidro", "San Vicente", "Santo Tomas", "Soom", "Tagum Norte", "Tagum Sur" }),
                CreateMunicipality("Tubigon", new[] { "Bagongbanwa", "Banlasan", "Batasan", "Bilangbilangan", "Bosongon", "Buenos Aires", "Bunacan", "Cabulihan", "Cahayag", "Cawayanan", "Centro", "Genonocan", "Guiwanon", "Ilihan Norte", "Ilihan Sur", "Libertad", "Macaas", "Matabao", "Mocaboc Island", "Panadtaran", "Panaytayon", "Pandan", "Pangapasan", "Pinayagan Norte", "Pinayagan Sur", "Pooc Occidental", "Pooc Oriental", "Potohan", "Talenceras", "Tan-awan", "Tinangnan", "Ubay Island", "Ubojan", "Villanueva" }),
                CreateMunicipality("Ubay", new[] { "Achila", "Bay-ang", "Benliw", "Biabas", "Bongbong", "Bood", "Buenavista", "Bulilis", "Cagting", "Calanggaman", "California", "Camali-an", "Camambugan", "Casate", "Cuya", "Fatima", "Gabi", "Governor Boyles", "Guintabo-an", "Hambabauran", "Humayhumay", "Ilihan", "Imelda", "Juagdan", "Katarungan", "Lomangog", "Los Angeles", "Pag-asa", "Pangpang", "Poblacion", "San Francisco", "San Isidro", "San Pascual", "San Vicente", "Sentinila", "Sinandigan", "Tapal", "Tapon", "Tintinan", "Tipolo", "Tubog", "Tuboran", "Union", "Villa Teresita" }),
                CreateMunicipality("Valencia", new[] { "Adlawan", "Anas", "Anonang", "Anoyon", "Balingasao", "Banderahan", "Botong", "Buyog", "Canduao Occidental", "Canduao Oriental", "Canlusong", "Canmanico", "Cansibao", "Catug-a", "Cutcutan", "Danao", "Genoveva", "Ginopolan", "La Victoria", "Lantang", "Limocon", "Loctob", "Magsaysay", "Marawis", "Maubo", "Nailo", "Omjon", "Pangi-an", "Poblacion Occidental", "Poblacion Oriental", "Simang", "Taug", "Tausion", "Taytay", "Ticum" })
            };

            var existingMunicipalities = await _db.Municipalities
                .Include(m => m.Barangays)
                .ToListAsync();

            foreach (var municipality in municipalities)
            {
                var existingMunicipality = existingMunicipalities
                    .FirstOrDefault(m => m.Name.ToLower() == municipality.Name.ToLower());

                if (existingMunicipality == null)
                {
                    _db.Municipalities.Add(municipality);
                    continue;
                }

                var existingBarangayNames = existingMunicipality.Barangays
                    .Select(b => b.Name.ToLower())
                    .ToHashSet();

                foreach (var barangay in municipality.Barangays)
                {
                    if (!existingBarangayNames.Contains(barangay.Name.ToLower()))
                    {
                        existingMunicipality.Barangays.Add(new Barangay { Name = barangay.Name });
                    }
                }
            }

            await _db.SaveChangesAsync();
        }

        private Municipality CreateMunicipality(string name, string[] barangayNames)
        {
            var barangays = barangayNames.Select(b => new Barangay { Name = b }).ToList();
            return new Municipality { Name = name, Barangays = barangays };
        }
    }
}
